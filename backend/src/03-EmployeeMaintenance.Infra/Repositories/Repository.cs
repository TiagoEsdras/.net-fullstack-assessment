using EmployeeMaintenance.Application.Interfaces.Repositories;
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
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
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
    }
}