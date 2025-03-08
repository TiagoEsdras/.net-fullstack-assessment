using EmployeeMaintenance.Application.Commands.Departments;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Services;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Shared.Enums;
using MediatR;

namespace EmployeeMaintenance.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMediator _mediator;

        public EmployeeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<EmployeeResposeDto> CreateEmployee(EmployeeRequestDto employeeRequest)
        {
            var department = await _mediator.Send(new GetDepartmentByNameQuery(employeeRequest.Department.Name));

            if (department.Status == ResultResponseKind.NotFound)
                await _mediator.Send(new CreateDepartmentCommand(employeeRequest.Department.Name));

            throw new NotImplementedException();
        }
    }
}