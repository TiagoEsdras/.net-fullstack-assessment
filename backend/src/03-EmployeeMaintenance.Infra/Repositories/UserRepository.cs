using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Infra.Persistence;

namespace EmployeeMaintenance.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EmployeeMaintenanceContext context) : base(context)
        {
        }
    }
}