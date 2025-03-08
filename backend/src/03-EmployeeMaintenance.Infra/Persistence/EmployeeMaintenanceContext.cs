using EmployeeMaintenance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMaintenance.Infra.Persistence
{
    public class EmployeeMaintenanceContext : DbContext
    {
        public EmployeeMaintenanceContext(DbContextOptions<EmployeeMaintenanceContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Query Filters

            modelBuilder.Entity<User>()
                .HasQueryFilter(e => !e.IsDeleted)
                .HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => !e.IsDeleted)
                .HasKey(e => e.Id);

            modelBuilder.Entity<Department>()
                .HasQueryFilter(e => !e.IsDeleted)
                .HasKey(e => e.Id);

            modelBuilder.Entity<Address>()
                .HasQueryFilter(e => !e.IsDeleted)
                .HasKey(e => e.Id);

            #endregion Query Filters

            #region Relationships

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne()
                .HasForeignKey<Address>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion Relationships

            #region Includes

            modelBuilder.Entity<Employee>()
                .Navigation(s => s.User)
                .AutoInclude();

            modelBuilder.Entity<Employee>()
               .Navigation(s => s.Department)
               .AutoInclude();

            modelBuilder.Entity<User>()
               .Navigation(s => s.Address)
               .AutoInclude();

            #endregion Includes
        }
    }
}