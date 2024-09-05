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

        public async Task<bool> AnyAsync(int id)
        {
            return await _dbSet.AnyAsync(x => x.CompanyID == id);
        }

        public async Task<List<Company>> GetAllWithDetailsAsync()
        {
            var companies = await _dbSet
                            .Include(x => x.Departments)
                            .Include(x => x.Ceo)
                            .ToListAsync();

            foreach (var company in companies)
            {
                company.Departments = company.Departments.Where(d => !d.IsDeleted).ToList();
            }

            return companies;
        }

        public async Task<Company> GetByIdWithDetailsAsync(int id)
        {
            var company = await _dbSet
                            .Include(x => x.Departments)
                            .Include(x => x.Ceo)
                            .FirstOrDefaultAsync(c => c.CompanyID == id);

            if (company != null)
            {
                company.Departments = company.Departments.Where(d => !d.IsDeleted).ToList();
            }

            return company;
        }
    }
}
