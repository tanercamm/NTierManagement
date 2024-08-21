using Microsoft.AspNetCore.Mvc;
using NTierManagement.BLL.DTOs.Department;
using NTierManagement.BLL.Interfaces;
using NTierManagement.Entity.Models;

namespace NTierManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Department
        [HttpGet("Details")]
        public async Task<ActionResult<List<Department>>> GetDepartmentsDetails()
        {
            var departments = await _departmentService.GetAllWithDetailsAsync();
            return Ok(departments);
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        // GET: api/Department/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department == null)
                return BadRequest();

            return Ok(department);
        }

        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult> AddDepartment(CreateDepartmentDTO dto)
        {
            await _departmentService.AddAsync(dto);
            return Ok();
        }

        // PUT: api/Department/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDepartment(UpdateDepartmentDTO dto)
        {
            if (dto.DepartmentID == null)
                return BadRequest();

            await _departmentService.UpdateAsync(dto);
            return NoContent(); // güncelleme sonrası veri dönmesin, gerekirse Get ile listesi istenebilir
        }

        // DELETE: api/Department/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            var department = _departmentService.GetByIdAsync(id);

            if (department == null)
                return BadRequest();

            await _departmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
