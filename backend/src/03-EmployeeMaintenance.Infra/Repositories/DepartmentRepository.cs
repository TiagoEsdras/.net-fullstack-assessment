using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMaintenance.Infra.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly EmployeeMaintenanceContext _context;

        public DepartmentRepository(EmployeeMaintenanceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Department?> GetDepartmentByNameAsync(string name)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Name == name);
        }
    }
}