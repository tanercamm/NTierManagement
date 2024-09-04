using Microsoft.EntityFrameworkCore;
using NTierManagement.Entity.Enums;
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
        public DbSet<Person> People { get; set; }

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
        public override int SaveChanges()
        {
            //ValidatePersons();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ValidatePersons();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ValidatePersons()
        {
            foreach (var entry in ChangeTracker.Entries<Person>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var person = entry.Entity;

                    if (person.Role == Roles.Ceo)
                    {
                        // CEO için: CompanyID dolu olmalı, DepartmentID boş olmalı
                        if (person.CompanyID == null || person.DepartmentID != null)
                        {
                            throw new InvalidOperationException("A CEO must have a valid CompanyID and DepartmentID must be null.");
                        }
                    }
                    else if (person.Role == Roles.Leader || person.Role == Roles.Employee)
                    {
                        // Leader ve Employee için: CompanyID ve DepartmentID dolu olmalı
                        if (person.CompanyID == null || person.DepartmentID == null)
                        {
                            throw new InvalidOperationException($"A {person.Role} must have both a valid CompanyID and DepartmentID.");
                        }
                    }
                    else if (person.Role == Roles.Employee)
                    {
                        // Employee için: CompanyID ve DepartmentID dolu olmalı
                        if (person.CompanyID == 0 || person.DepartmentID == null)
                        {
                            throw new InvalidOperationException("An Employee must have both a valid CompanyID and DepartmentID.");
                        }
                    }
                }
            }
        }
    }
}
