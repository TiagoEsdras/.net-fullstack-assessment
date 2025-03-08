using AutoMapper;
using EmployeeMaintenance.Application.Commands.Departments;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Interfaces.Services;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IMediator mediator, IMapper mapper, IRepository<Employee> repository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<EmployeeResponseDto> CreateEmployee(EmployeeRequestDto employeeRequest)
        {
            #region Department

            var department = await _mediator.Send(new GetDepartmentByNameQuery(employeeRequest.Department.Name));

            if (department.Status == ResultResponseKind.NotFound)
                department = await _mediator.Send(new CreateDepartmentCommand(employeeRequest.Department.Name));

            #endregion Department

            #region Create User

            var createUserCommand = _mapper.Map<CreateUserCommand>(employeeRequest.User);
            var user = await _mediator.Send(createUserCommand);

            #endregion Create User

            #region Create Employee

            var createEmployeeCommand = new CreateEmployeeCommand(employeeRequest.HireDate, user.Data.Id, department.Data.Id);
            var employee = await _mediator.Send(createEmployeeCommand);

            #endregion Create Employee

            await _repository.SaveAsync();
            return _mapper.Map<EmployeeResponseDto>(employee.Data);
        }
    }
}