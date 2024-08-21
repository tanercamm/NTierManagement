using Microsoft.EntityFrameworkCore;
using NTierManagement.DAL.Abstract;
using NTierManagement.Entity.Context;
using NTierManagement.Entity.Models;

namespace NTierManagement.DAL.Concrete
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _dbSet;

        public DepartmentRepository(ManagementContext context) : base(context)
        {
            _dbSet = context.Set<Department>();
        }

        public async Task<List<Department>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                            .Include(c => c.Company)
                            .Include(l => l.Leader)
                            .Include(p => p.People)
                            .ToListAsync();
        }

        public async Task<Department> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                            .Include(c => c.Company)
                            .Include(l => l.Leader)
                            .Include(p => p.People)
                            .FirstOrDefaultAsync(d => d.DepartmentID == id);

        }
    }
}
