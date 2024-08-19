using Microsoft.EntityFrameworkCore;
using NTierManagement.Entity.Models;

namespace NTierManagement.Entity.Context
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Company - CEO relation
            modelBuilder.Entity<Company>()
                .HasOne(c => c.Ceo)
                .WithMany()
                .HasForeignKey(c => c.CeoID)
                .OnDelete(DeleteBehavior.Restrict);

            // Department - Leader relation
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Leader)
                .WithMany()
                .HasForeignKey(d => d.LeaderID)
                .OnDelete(DeleteBehavior.Restrict);

            // Person - Department relation
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Department)
                .WithMany(d => d.People)
                .HasForeignKey(p => p.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
