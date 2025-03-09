using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Shared;
using MediatR;

namespace EmployeeMaintenance.Application.Commands.Employees
{
    public class UpdateEmployeeDepartmentCommand : IRequest<Result<EmployeeResponseDto>>
    {
        public UpdateEmployeeDepartmentCommand()
        {
        }

        public UpdateEmployeeDepartmentCommand(Guid employeeId, DepartmentResponseDto department)
        {
            EmployeeId = employeeId;
            Department = department;
        }

        public Guid EmployeeId { get; set; }

        public DepartmentResponseDto Department { get; set; }
    }
}