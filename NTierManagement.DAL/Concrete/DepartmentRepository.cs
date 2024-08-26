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

        public async Task<bool> AnyAsync(int id)
        {
            return await _dbSet.AnyAsync(x => x.DepartmentID == id);
        }

        public async Task<List<Department>> GetAllWithDetailsAsync()
        {
            var departments = await _dbSet
                            .Include(c => c.Company)
                            .Include(d => d.Leader)
                            .Include(d => d.People)
                            .ToListAsync();

            foreach (var department in departments)
            {
                department.People = department.People.Where(p => !p.IsDeleted).ToList();
            }

            return departments;
        }

        public async Task<Department> GetByIdWithDetailsAsync(int id)
        {
            var department = await _dbSet
                            .Include(c => c.Company)
                            .Include(d => d.Leader)
                            .Include(d => d.People)
                            .FirstOrDefaultAsync(d => d.DepartmentID == id);

            if (department != null)
            {
                department.People = department.People.Where(p => !p.IsDeleted).ToList();
            }

            return department;

        }
    }
}
