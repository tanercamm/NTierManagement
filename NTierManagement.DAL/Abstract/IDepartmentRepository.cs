using NTierManagement.Entity.Models;

namespace NTierManagement.DAL.Abstract
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<List<Department>> GetAllWithDetailsAsync();

        Task<Department> GetByIdWithDetailsAsync(int id);

        Task<bool> AnyAsync(int id);
    }
}
