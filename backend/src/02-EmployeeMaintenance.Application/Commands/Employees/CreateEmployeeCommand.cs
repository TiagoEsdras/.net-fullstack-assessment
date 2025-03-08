using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Commands.Employees
{
    public class CreateEmployeeCommand : IRequest<Result<Employee>>
    {
        public CreateEmployeeCommand(DateTime hireDate, Guid userId, Guid departmentId)
        {
            HireDate = hireDate;
            UserId = userId;
            DepartmentId = departmentId;
        }

        public DateTime HireDate { get; set; }

        public Guid UserId { get; private set; }

        public Guid DepartmentId { get; set; }

        public Employee ToEntity()
            => new(HireDate, UserId, DepartmentId);
    }
}