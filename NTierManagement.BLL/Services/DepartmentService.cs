using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.DTOs.Department;
using NTierManagement.BLL.DTOs.Person;
using NTierManagement.BLL.Interfaces;
using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Context;
using NTierManagement.Entity.Models;

namespace NTierManagement.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ManagementContext _managementContext;

        public DepartmentService(IDepartmentRepository departmentRepository, ICompanyRepository companyRepository, IPersonRepository personRepository, ManagementContext managementContext)
        {
            _departmentRepository = departmentRepository;
            _companyRepository = companyRepository;
            _personRepository = personRepository;
            _managementContext = managementContext;
        }

        public async Task<List<DepartmentDTO>> GetAllWithDetailsAsync()
        {
            var departmentEntities = await _departmentRepository.GetAllWithDetailsAsync();

            var list = new List<DepartmentDTO>();

            foreach (var departmentEntity in departmentEntities.Where(t => !t.IsDeleted))
            {
                list.Add(new DepartmentDTO
                {
                    DepartmentID = departmentEntity.DepartmentID,
                    Subject = departmentEntity.Subject,
                    Capacity = departmentEntity.Capacity,
                    PhoneNumber = departmentEntity.PhoneNumber,
                    Company = new CompanyBaseDTO
                    {
                        CompanyID = departmentEntity.Company.CompanyID,
                        CompanyName = departmentEntity.Company.CompanyName,
                        Address = departmentEntity.Company.Address,
                        Email = departmentEntity.Company.Email,
                        PhoneNumber = departmentEntity.Company.PhoneNumber
                    },
                    People = departmentEntity.People.Select(p => new PersonBaseDTO
                    {
                        PersonID = p.PersonID,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Age = p.Age,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Role = p.Role
                    }).ToList()
                });
            }
            return list;
        }

        public async Task<List<DepartmentBaseDTO>> GetAllAsync()
        {
            var departmentEntities = await _departmentRepository.GetAllAsync();

            var list = new List<DepartmentBaseDTO>();

            foreach (var departmentEntity in departmentEntities.Where(t => !t.IsDeleted))
            {
                list.Add(new DepartmentBaseDTO
                {
                    DepartmentID = departmentEntity.DepartmentID,
                    Subject = departmentEntity.Subject,
                    Capacity = departmentEntity.Capacity,
                    PhoneNumber = departmentEntity.PhoneNumber
                });
            }
            return list;
        }

        public async Task<DepartmentDTO> GetByIdAsync(int id)
        {
            var departmentEntity = await _departmentRepository.GetByIdWithDetailsAsync(id);

            if (departmentEntity == null || departmentEntity.IsDeleted)
                throw new Exception("Department not found!");

            var departmentDto = new DepartmentDTO
            {
                DepartmentID = departmentEntity.DepartmentID,
                Subject = departmentEntity.Subject,
                Capacity= departmentEntity.Capacity,
                PhoneNumber = departmentEntity.PhoneNumber,
                Company = new CompanyBaseDTO
                {
                    CompanyID = departmentEntity.Company.CompanyID,
                    CompanyName = departmentEntity.Company.CompanyName,
                    Address = departmentEntity.Company.Address,
                    Email = departmentEntity.Company.Email,
                    PhoneNumber = departmentEntity.Company.PhoneNumber
                },
                People = departmentEntity.People.Select(p => new PersonBaseDTO {
                    PersonID = p.PersonID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    PhoneNumber= p.PhoneNumber,
                    Email= p.Email,
                    Role = p.Role
                }).ToList()
            };
            return departmentDto;
        }

        public async Task AddAsync(CreateDepartmentDTO dto)
        {
            var isCompanyExist = await _companyRepository.AnyAsync(dto.CompanyID);

            if (!isCompanyExist)
                throw new Exception("Company does not exist!");

            var leader = new Person
            {
                FirstName = dto.Leader.FirstName,
                LastName = dto.Leader.LastName,
                Age = dto.Leader.Age,
                PhoneNumber = dto.Leader.PhoneNumber,
                Email = dto.Leader.Email,
                CompanyID = dto.CompanyID,
                Role = (Entity.Enums.Roles)1
            };

            await _personRepository.AddAsync(leader);

            var departmentEntity = new Department
            {
                Subject = dto.Subject,
                Capacity = dto.Capacity,
                PhoneNumber = dto.PhoneNumber,
                CompanyID = dto.CompanyID
            };

            leader.Department = departmentEntity;
            departmentEntity.AddLeader(leader);

            await _departmentRepository.AddAsync(departmentEntity);
        }

        public Task UpdateAsync(UpdateDepartmentDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
