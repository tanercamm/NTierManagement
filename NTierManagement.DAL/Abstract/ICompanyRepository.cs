using NTierManagement.Entity.Models;

namespace NTierManagement.DAL.Abstract
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<List<Company>> GetAllWithDetailsAsync();

        Task<Company> GetByIdWithDetailsAsync(int id);
    }
}