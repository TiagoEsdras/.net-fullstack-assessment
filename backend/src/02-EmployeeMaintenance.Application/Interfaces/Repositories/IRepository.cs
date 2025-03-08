namespace EmployeeMaintenance.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<int> CountAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);

        T? Update(T entity);

        Task<bool> DeleteAsync(Guid id);

        Task SaveAsync();
    }
}