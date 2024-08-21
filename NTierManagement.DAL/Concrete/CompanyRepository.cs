using Microsoft.EntityFrameworkCore;
using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Context;
using NTierManagement.Entity.Models;

namespace NTierManagement.DAL.Concrete
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly DbSet<Company> _dbSet;

        public CompanyRepository(ManagementContext context) : base(context)
        {
            _dbSet = context.Set<Company>();
        }

        public async Task<List<Company>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                            .Include(x => x.Departments)
                            .ToListAsync();
        }

        public async Task<Company> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                            .Include(x => x.Departments)
                            .FirstOrDefaultAsync(d => d.CompanyID == id);
        }
    }
}
