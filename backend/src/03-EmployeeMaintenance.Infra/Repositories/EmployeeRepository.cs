using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMaintenance.Infra.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly EmployeeMaintenanceContext _context;

        public EmployeeRepository(EmployeeMaintenanceContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Employees.FindAsync(id);
            if (entity == null)
                return false;

            entity.SoftDelete();
            entity.User.SoftDelete();
            entity.User.Address.SoftDelete();
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}