namespace EmployeeMaintenance.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        T? Update(T entity);

        Task<bool> DeleteAsync(Guid id);

        Task SaveAsync();
    }
}