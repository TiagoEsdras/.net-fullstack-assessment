using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Infra.Persistence;

namespace EmployeeMaintenance.Infra.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeMaintenanceContext context) : base(context)
        {
        }
    }
}