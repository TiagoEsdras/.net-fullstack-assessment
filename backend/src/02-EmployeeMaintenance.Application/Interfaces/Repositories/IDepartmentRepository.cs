using EmployeeMaintenance.Domain.Entities;

namespace EmployeeMaintenance.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department?> GetDepartmentByNameAsync(string name);
    }
}