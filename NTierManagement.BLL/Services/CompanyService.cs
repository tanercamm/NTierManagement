using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.DTOs.Department;
using NTierManagement.BLL.DTOs.Person;
using NTierManagement.BLL.Interfaces;
using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Models;

namespace NTierManagement.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
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
                CompanyID= companyEntity.CompanyID,
                CompanyName= companyEntity.CompanyName,
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
            var companyEntity = new Company
            {
                CompanyName = dto.CompanyName,
                Address = dto.Address,
                Email= dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CeoID = dto.CeoID
            };

            await _companyRepository.AddAsync(companyEntity);
        }

        public Task UpdateAsync(UpdateCompanyDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
