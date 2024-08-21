using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.DTOs.Department;
using NTierManagement.BLL.DTOs.Person;
using NTierManagement.BLL.Interfaces;
using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Models;

namespace NTierManagement.BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public PersonService(IPersonRepository personRepository, ICompanyRepository companyRepository, IDepartmentRepository departmentRepository)
        {
            _personRepository = personRepository;
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<List<PersonDTO>> GetAllWithDetailsAsync()
        {
            var personEntities = await _personRepository.GetAllWithDetailsAsync();

            var list = new List<PersonDTO>();

            foreach (var personEntity in personEntities)
            {
                list.Add(new PersonDTO
                {
                    PersonID = personEntity.PersonID,
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName,
                    Age = personEntity.Age,
                    Email = personEntity.Email,
                    PhoneNumber = personEntity.PhoneNumber,
                    Role = personEntity.Role,
                    Company = new CompanyBaseDTO
                    {
                        CompanyID = personEntity.Company.CompanyID,
                        CompanyName = personEntity.Company.CompanyName,
                        Address = personEntity.Company.Address,
                        Email = personEntity.Company.Email,
                        PhoneNumber = personEntity.Company.PhoneNumber
                    },
                    Department = new DepartmentBaseDTO
                    {
                        DepartmentID = personEntity.Department.DepartmentID,
                        Subject = personEntity.Department.Subject,
                        Capacity = personEntity.Department.Capacity,
                        PhoneNumber = personEntity.Department.PhoneNumber
                    }
                });
            }
            return list;
        }

        public async Task<List<PersonBaseDTO>> GetAllAsync()
        {
            var personEntities = await _personRepository.GetAllAsync();

            var list = new List<PersonBaseDTO>();

            foreach (var personEntity in personEntities)
            {
                list.Add(new PersonBaseDTO
                {
                    PersonID = personEntity.PersonID,
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName,
                    Age = personEntity.Age,
                    Email = personEntity.Email,
                    PhoneNumber = personEntity.PhoneNumber,
                    Role = personEntity.Role
                });
            }
            return list;
        }

        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var personEntity = await _personRepository.GetByIdWithDetailsAsync(id);

            if (personEntity == null)
                throw new Exception("Person not found!");

            var personDto = new PersonDTO
            {
                PersonID = personEntity.PersonID,
                FirstName = personEntity.FirstName,
                LastName = personEntity.LastName,
                Age = personEntity.Age,
                Email = personEntity.Email,
                PhoneNumber = personEntity.PhoneNumber,
                Role = personEntity.Role,
                Company = new CompanyBaseDTO
                {
                    CompanyID = personEntity.Company.CompanyID,
                    CompanyName = personEntity.Company.CompanyName,
                    Address = personEntity.Company.Address,
                    Email = personEntity.Company.Email,
                    PhoneNumber = personEntity.Company.PhoneNumber
                },
                Department = new DepartmentBaseDTO
                {
                    DepartmentID = personEntity.Department.DepartmentID,
                    Subject = personEntity.Department.Subject,
                    Capacity = personEntity.Department.Capacity,
                    PhoneNumber = personEntity.Department.PhoneNumber
                }
            };
            return personDto;
        }

        public async Task AddAsync(CreatePersonDTO dto)
        {
            var isCompanyExist = await _companyRepository.AnyAsync(dto.CompanyID);
            var isDepartmentExist = await _departmentRepository.AnyAsync(dto.DepartmentID);

            if (!isCompanyExist)
                throw new Exception("Company does not exist!");
            if (!isDepartmentExist)
                throw new Exception("Department does not exist!");

            var entity = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Age = dto.Age,
                Role = dto.Role
            };
            await _personRepository.AddAsync(entity);
        }

        public Task UpdateAsync(UpdatePersonDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
