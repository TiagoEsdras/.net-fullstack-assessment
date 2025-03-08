using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMaintenance.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EmployeeMaintenanceContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(EmployeeMaintenanceContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task SaveAsync()
        {
            HandleTimestamps();
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            if (entity is not BaseEntity baseEntity)
            {
                _dbSet.Remove(entity);
                return true;
            }

            baseEntity.SoftDelete();
            _dbSet.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) =>
            await _dbSet.FindAsync(id);

        public T? Update(T entity)
        {
            _dbSet.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        private void HandleTimestamps()
        {
            var entries = _context
                .ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            var now = DateTime.UtcNow;
            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                    entity.SetCreatedAt(now);
                else
                    entity.SetUpdatedAt(now);
            }
        }
    }
}