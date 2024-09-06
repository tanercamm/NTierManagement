using Microsoft.EntityFrameworkCore;
using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Context;
using NTierManagement.Entity.Enums;
using NTierManagement.Entity.Models;

namespace NTierManagement.DAL.Concrete
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly DbSet<Person> _dbSet;

        public PersonRepository(ManagementContext context) : base(context)
        {
            _dbSet = context.Set<Person>();
        }

        public async Task<List<Person>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                            .Include(c => c.Company)
                            .Include(d => d.Department)
                            .Where(p => !p.IsDeleted)
                            .ToListAsync();
        }

        public async Task<Person> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                            .Include(c => c.Company)
                            .Include(d => d.Department)
                            .FirstOrDefaultAsync(p => p.PersonID == id && !p.IsDeleted);
        }

        public async Task<Person> GetByCompanyIdAndRoleAsync(int companyId, Roles role)
        {
            return await _dbSet
                            .FirstOrDefaultAsync(p => p.CompanyID == companyId && p.Role == role && !p.IsDeleted);
        }
    }
}
