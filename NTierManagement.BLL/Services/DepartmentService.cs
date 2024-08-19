using NTierManagement.BLL.DTOs.Department;
using NTierManagement.BLL.Interfaces;

namespace NTierManagement.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {

        public Task<List<DepartmentDTO>> GetAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartmentBaseDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(CreateDepartmentDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateDepartmentDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
