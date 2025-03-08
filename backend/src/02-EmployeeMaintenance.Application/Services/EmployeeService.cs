using AutoMapper;
using EmployeeMaintenance.Application.Commands.Departments;
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

        public async Task<EmployeeResposeDto> CreateEmployee(EmployeeRequestDto employeeRequest)
        {
            var department = await _mediator.Send(new GetDepartmentByNameQuery(employeeRequest.Department.Name));

            if (department.Status == ResultResponseKind.NotFound)
                await _mediator.Send(new CreateDepartmentCommand(employeeRequest.Department.Name));

            var createUserCommand = _mapper.Map<CreateUserCommand>(employeeRequest.User);
            await _mediator.Send(createUserCommand);
            await _repository.SaveAsync();
            throw new NotImplementedException();
        }
    }
}