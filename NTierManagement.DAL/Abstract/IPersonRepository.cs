using NTierManagement.Entity.Enums;
using NTierManagement.Entity.Models;

namespace NTierManagement.DAL.Abstract
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Person>> GetAllWithDetailsAsync();

        Task<Person> GetByIdWithDetailsAsync(int id);

        Task<Person> GetByCompanyIdAndRoleAsync(int companyId, Roles role);
    }
}
