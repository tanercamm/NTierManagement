using Microsoft.AspNetCore.Mvc;
using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.Interfaces;
using NTierManagement.Entity.Models;

namespace NTierManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService service)
        {
            _companyService = service;
        }

        // GET: api/Company
        [HttpGet("Details")]
        public async Task<ActionResult<List<Company>>> GetCompaniesDetails()
        {
            var companies = await _companyService.GetAllWithDetailsAsync();
            return Ok(companies);
        }

        // GET: api/Company
        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetCompanies()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        // GET: api/Company/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetByIdAsync(id);

            if (company == null)
                return NotFound();

            return Ok(company);
        }

        // POST: api/Company
        [HttpPost]
        public async Task<ActionResult> AddCompany(CreateCompanyDTO dto)
        {
            await _companyService.AddAsync(dto);
            return Ok();
        }

        // PUT: api/Company/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCompany(UpdateCompanyDTO dto)
        {
            if (dto.CompanyID == null)
                return BadRequest();

            await _companyService.UpdateAsync(dto);
            return NoContent(); // güncelleme sonrası veri dönmesin, gerekirse Get ile listesi istenebilir
        }

        // DELETE: api/Company/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            var company = _companyService.GetByIdAsync(id);
        
            if(company == null)
                return BadRequest();

            await _companyService.DeleteAsync(id);
            return NoContent();
        }

    }
}
