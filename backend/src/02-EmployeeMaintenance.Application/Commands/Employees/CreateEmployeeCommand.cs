using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Commands.Employees
{
    public class CreateEmployeeCommand : IRequest<Result<EmployeeResponseDto>>
    {
        public CreateEmployeeCommand()
        {
        }

        public CreateEmployeeCommand(DateTime hireDate, Guid userId, Guid departmentId)
        {
            HireDate = hireDate;
            UserId = userId;
            DepartmentId = departmentId;
        }

        public DateTime HireDate { get; set; }

        public Guid UserId { get; set; }

        public Guid DepartmentId { get; set; }

        public Employee ToEntity()
            => new(HireDate, UserId, DepartmentId);
    }
}