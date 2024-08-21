using Microsoft.AspNetCore.Mvc;
using NTierManagement.BLL.DTOs.Person;
using NTierManagement.BLL.Interfaces;
using NTierManagement.Entity.Models;

namespace NTierManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/Person
        [HttpGet("Details")]
        public async Task<ActionResult<List<Person>>> GetPeopleDetails()
        {
            var people = await _personService.GetAllWithDetailsAsync();
            return Ok(people);
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPeople()
        {
            var people = await _personService.GetAllAsync();
            return Ok(people);
        }

        // GET: api/Person/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _personService.GetByIdAsync(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST: api/Person
        [HttpPost]
        public async Task<ActionResult> AddPerson(CreatePersonDTO dto)
        {
            await _personService.AddAsync(dto);
            return Ok();
        }

        // PUT: api/Person/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson(UpdatePersonDTO dto)
        {
            if (dto.PersonID == null)
                return BadRequest();

            await _personService.UpdateAsync(dto);
            return NoContent(); // güncelleme sonrası veri dönmesin, gerekirse Get ile listesi istenebilir
        }

        // DELETE: api/Person/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = _personService.GetByIdAsync(id);

            if (person == null)
                return BadRequest();

            await _personService.DeleteAsync(id);
            return NoContent();
        }

    }
}
