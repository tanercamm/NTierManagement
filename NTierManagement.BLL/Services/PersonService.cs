using NTierManagement.BLL.DTOs.Company;
using NTierManagement.BLL.DTOs.Department;
using NTierManagement.BLL.DTOs.Person;
using NTierManagement.BLL.Interfaces;
using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Enums;
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

            foreach (var personEntity in personEntities.Where(t => !t.IsDeleted))
            {
                if (personEntity.Role == Roles.Ceo)
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
                        Company = personEntity.Company != null ? new CompanyBaseDTO
                        {
                            CompanyID = personEntity.Company.CompanyID,
                            CompanyName = personEntity.Company.CompanyName,
                            Address = personEntity.Company.Address,
                            Email = personEntity.Company.Email,
                            PhoneNumber = personEntity.Company.PhoneNumber
                        } : null
                    });
                }
                else if (personEntity.Role == Roles.Leader || personEntity.Role == Roles.Employee)
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
                        Company = personEntity.Company != null ? new CompanyBaseDTO
                        {
                            CompanyID = personEntity.Company.CompanyID,
                            CompanyName = personEntity.Company.CompanyName,
                            Address = personEntity.Company.Address,
                            Email = personEntity.Company.Email,
                            PhoneNumber = personEntity.Company.PhoneNumber
                        } : null,
                        Department = new DepartmentBaseDTO
                        {
                            DepartmentID = personEntity.Department.DepartmentID,
                            Subject = personEntity.Department.Subject,
                            Capacity = personEntity.Department.Capacity,
                            PhoneNumber = personEntity.Department.PhoneNumber
                        }
                    });
                }
            }
            return list;
        }

        public async Task<List<PersonBaseDTO>> GetAllAsync()
        {
            var personEntities = await _personRepository.GetAllAsync();

            var list = new List<PersonBaseDTO>();

            foreach (var personEntity in personEntities.Where(t => !t.IsDeleted))
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

            if (personEntity == null || personEntity.IsDeleted)
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
                }
            };

            if (personEntity.Role == Roles.Leader || personEntity.Role == Roles.Employee)
            {

                personDto.Department = new DepartmentBaseDTO
                {
                    DepartmentID = personEntity.Department.DepartmentID,
                    Subject = personEntity.Department.Subject,
                    Capacity = personEntity.Department.Capacity,
                    PhoneNumber = personEntity.Department.PhoneNumber
                };
            }
            return personDto;
        }

        public async Task AddAsync(CreatePersonDTO dto)
        {
            bool isDepartmentExist = true;
            if (dto.Role != Roles.Ceo && dto.DepartmentID.HasValue)
            {
                var department = await _departmentRepository.GetByIdAsync(dto.DepartmentID.Value);
                isDepartmentExist = department != null;
            }
            else if (dto.Role != Roles.Ceo && !dto.DepartmentID.HasValue)
            {
                isDepartmentExist = false;
            }

            if ((dto.Role == Roles.Leader || dto.Role == Roles.Employee) && !isDepartmentExist)
            {
                throw new Exception("Department does not exist!");
            }

            // DTO validasyonları
            if (dto.Role == Roles.Ceo && dto.DepartmentID != null)
            {
                throw new InvalidOperationException("A CEO cannot be assigned to a department.");
            }

            if ((dto.Role == Roles.Leader || dto.Role == Roles.Employee) && dto.DepartmentID == null)
            {
                throw new InvalidOperationException("A Leader or Employee must have a DepartmentID.");
            }

            // DTO'dan Person nesnesi oluşturulması
            var entity = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Age = dto.Age,
                Role = dto.Role,
                CompanyID = dto.CompanyID,
                DepartmentID = dto.DepartmentID
            };

            await _personRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdatePersonDTO dto)
        {
            var personEntity = await _personRepository.GetByIdAsync(dto.PersonID);

            if (personEntity == null || personEntity.IsDeleted)
                throw new Exception("Person not found!");

            if (personEntity.Role != dto.Role)
            {
                if (dto.Role == Roles.Ceo)
                {
                    // CEO için sadece CompanyID güncellenir, DepartmentID null olur
                    if (dto.DepartmentID != null)
                        throw new InvalidOperationException("A CEO cannot be assigned to a department.");

                    // Şirketteki mevcut konumda bir CEO olup olmadığı kontrolü
                    var existingCeo = await _personRepository.GetByCompanyIdAndRoleAsync(dto.CompanyID.Value, Roles.Ceo);

                    if (existingCeo != null && existingCeo.PersonID != dto.PersonID)
                    {
                        // Mevcut CEO'yu isDeleted = true olarak işaretleyin
                        existingCeo.Delete();   // Person Enity içerisinde oluşturduğumuz Delete metodu ile isDeleted = true olur
                        await _personRepository.UpdateAsync(existingCeo);
                    }

                    personEntity.CompanyID = dto.CompanyID;
                    personEntity.DepartmentID = null;

                    // Şirket tablosunda CeoID yeni atanan kişinin id ile değiştirilir
                    var company = await _companyRepository.GetByIdAsync(dto.CompanyID.Value);
                    if (company != null)
                    {
                        company.CeoID = dto.PersonID;
                        await _companyRepository.UpdateAsync(company);
                    }
                }
                else if (dto.Role == Roles.Leader)
                {
                    // Mevcut şirkette bir Leader olup olmadığı kontrolü
                    var existingLeader = await _personRepository.GetByCompanyIdAndRoleAsync(dto.CompanyID.Value, Roles.Leader);

                    if (existingLeader != null && existingLeader.PersonID != dto.PersonID)
                    {
                        // Mevcut Leader'ı isDeleted = true olarak işaretleyin
                        existingLeader.Delete();
                        await _personRepository.UpdateAsync(existingLeader);
                    }

                    // Leader için hem CompanyID hem de DepartmentID zorunlu olmalı
                    if (dto.DepartmentID == null)
                        throw new InvalidOperationException("A Leader must have a DepartmentID.");

                    var departmentExists = await _departmentRepository.GetByIdAsync(dto.DepartmentID.Value) != null;
                    if (!departmentExists)
                        throw new Exception("Department does not exist!");

                    personEntity.CompanyID = dto.CompanyID;
                    personEntity.DepartmentID = dto.DepartmentID;

                    // Department tablosunda LeaderID yeni atanan kişinin id ile değiştirilir
                    var department = await _departmentRepository.GetByIdAsync(dto.DepartmentID.Value);
                    if (department != null)
                    {
                        department.LeaderID = dto.PersonID;
                        await _departmentRepository.UpdateAsync(department);
                    }
                }
                else if (dto.Role == Roles.Employee)
                {
                    // Employee için hem CompanyID hem de DepartmentID zorunlu olmalı
                    if (dto.DepartmentID == null)
                        throw new InvalidOperationException("An Employee must have a DepartmentID.");

                    var departmentExists = await _departmentRepository.GetByIdAsync(dto.DepartmentID.Value) != null;
                    if (!departmentExists)
                        throw new Exception("Department does not exist!");

                    personEntity.CompanyID = dto.CompanyID;
                    personEntity.DepartmentID = dto.DepartmentID;
                }
                personEntity.Role = dto.Role;
            }

            personEntity.FirstName = dto.FirstName;
            personEntity.LastName = dto.LastName;
            personEntity.Email = dto.Email;
            personEntity.PhoneNumber = dto.PhoneNumber;
            personEntity.Age = dto.Age;

            await _personRepository.UpdateAsync(personEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person == null || person.IsDeleted)
                throw new Exception($"Unable to delete {id}");

            person.Delete();
            await _personRepository.UpdateAsync(person);
        }
    }
}
