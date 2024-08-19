using NTierManagement.BLL.DTOs.Department;

namespace NTierManagement.BLL.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> GetAllWithDetails();

        Task<List<DepartmentBaseDTO>> GetAllAsync();

        Task<DepartmentDTO> GetByIdAsync(int id);

        Task AddAsync(CreateDepartmentDTO entity);

        Task UpdateAsync(UpdateDepartmentDTO entity);

        Task DeleteAsync(int id);
    }
}
