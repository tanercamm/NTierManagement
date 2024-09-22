using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.DTOs.Department;
using NTierManagement.BLL.DTOs.Person;
using NTierManagement.BLL.Interfaces;
using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Context;
using NTierManagement.Entity.Enums;
using NTierManagement.Entity.Models;

namespace NTierManagement.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ManagementContext _managementContext;

        public CompanyService(ICompanyRepository companyRepository, IDepartmentRepository departmentRepository, IPersonRepository personRepository, ManagementContext managementContext)
        {
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;
            _personRepository = personRepository;
            _managementContext = managementContext;
        }

        public async Task<List<CompanyDTO>> GetAllWithDetailsAsync()
        {
            var companyEntities = await _companyRepository.GetAllWithDetailsAsync();

            var list = new List<CompanyDTO>();

            foreach (var companyEntity in companyEntities.Where(t => !t.IsDeleted))
            {
                list.Add(new CompanyDTO
                {
                    CompanyID = companyEntity.CompanyID,
                    CompanyName = companyEntity.CompanyName,
                    Address = companyEntity.Address,
                    Email = companyEntity.Email,
                    PhoneNumber = companyEntity.PhoneNumber,
                    Ceo = new PersonBaseDTO
                    {
                        PersonID = companyEntity.Ceo.PersonID,
                        FirstName = companyEntity.Ceo.FirstName,
                        LastName = companyEntity.Ceo.LastName,
                        Age = companyEntity.Ceo.Age,
                        Email = companyEntity.Ceo.Email,
                        PhoneNumber = companyEntity.Ceo.PhoneNumber,
                        Role = companyEntity.Ceo.Role
                    },
                    Departments = companyEntity.Departments
                                  .Select(c => new DepartmentBaseDTO
                                  {
                                      DepartmentID = c.DepartmentID,
                                      Subject = c.Subject,
                                      Capacity = c.Capacity,
                                      PhoneNumber = c.PhoneNumber
                                  })
                                  .ToList()
                });
            }
            return list;
        }

        public async Task<List<CompanyBaseDTO>> GetAllAsync()
        {
            var companyEntities = await _companyRepository.GetAllAsync();

            var list = new List<CompanyBaseDTO>();

            foreach (var companyEntity in companyEntities.Where(t => !t.IsDeleted))
            {
                list.Add(new CompanyBaseDTO
                {
                    CompanyID = companyEntity.CompanyID,
                    CompanyName = companyEntity.CompanyName,
                    Address = companyEntity.Address,
                    Email = companyEntity.Email,
                    PhoneNumber = companyEntity.PhoneNumber
                });
            }
            return list;
        }

        public async Task<CompanyDTO> GetByIdAsync(int id)
        {
            var companyEntity = await _companyRepository.GetByIdWithDetailsAsync(id);

            if (companyEntity == null || companyEntity.IsDeleted)
                throw new Exception("Company not found!");

            var companyDto = new CompanyDTO
            {
                CompanyID = companyEntity.CompanyID,
                CompanyName = companyEntity.CompanyName,
                Address = companyEntity.Address,
                Email = companyEntity.Email,
                PhoneNumber = companyEntity.PhoneNumber,
                Ceo = new PersonBaseDTO
                {
                    PersonID = companyEntity.Ceo.PersonID,
                    FirstName = companyEntity.Ceo.FirstName,
                    LastName = companyEntity.Ceo.LastName,
                    Age = companyEntity.Ceo.Age,
                    Email = companyEntity.Ceo.Email,
                    PhoneNumber = companyEntity.Ceo.PhoneNumber,
                    Role = companyEntity.Ceo.Role
                },
                Departments = companyEntity.Departments.Select(d => new DepartmentBaseDTO
                {
                    DepartmentID = d.DepartmentID,
                    Subject = d.Subject,
                    Capacity = d.Capacity,
                    PhoneNumber = d.PhoneNumber
                }).ToList()
            };
            return companyDto;
        }

        public async Task AddAsync(CreateCompanyDTO dto)
        {
            var ceo = new Person
            {
                FirstName = dto.Ceo.FirstName,
                LastName = dto.Ceo.LastName,
                Email = dto.Ceo.Email,
                PhoneNumber = dto.Ceo.PhoneNumber,
                Age = dto.Ceo.Age
            };

            await _personRepository.AddAsync(ceo);

            var companyEntity = new Company
            {
                CompanyName = dto.CompanyName,
                Address = dto.Address,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
            };

            ceo.Company = companyEntity;
            companyEntity.AddCeo(ceo);

            await _companyRepository.AddAsync(companyEntity);
        }

        public async Task UpdateAsync(UpdateCompanyDTO dto)
        {
            var companyEntity = await _companyRepository.GetByIdAsync(dto.CompanyID);

            if (companyEntity == null || companyEntity.IsDeleted)
                throw new Exception("Company not found!");

            var oldCeo = await _personRepository.GetByIdAsync(companyEntity.CeoID);
            oldCeo.CompanyID = null;
            oldCeo.Delete();
            await _personRepository.UpdateAsync(oldCeo);

            companyEntity.CompanyName = dto.CompanyName;
            companyEntity.Address = dto.Address;
            companyEntity.Email = dto.Email;
            companyEntity.PhoneNumber = dto.PhoneNumber;
            companyEntity.CeoID = (int)dto.CeoID;

            var newCeo = await _personRepository.GetByIdAsync((int) dto.CeoID);
            newCeo.Role = Roles.Ceo;
            newCeo.DepartmentID = null;
            newCeo.CompanyID = companyEntity.CompanyID;
            await _personRepository.UpdateAsync(newCeo);

            await _companyRepository.UpdateAsync(companyEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null || company.IsDeleted)
                throw new Exception($"Unable to delete {id}");

            var departmentList = await _departmentRepository.GetAllByCompanyIdWithDetails(id);

            departmentList.ForEach(x => x.Delete());

            company.Delete();

            foreach (var departmentEntity in departmentList)
            {
                await _departmentRepository.UpdateAsync(departmentEntity);
            }

            await _companyRepository.UpdateAsync(company);
        }
    }
}
