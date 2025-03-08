using AutoMapper;
using EmployeeMaintenance.Application.Commands.Departments;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Interfaces.Services;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using FluentValidation;
using MediatR;

namespace EmployeeMaintenance.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRepository<Employee> _repository;
        private readonly IValidator<EmployeeRequestDto> _validator;

        public EmployeeService(IMediator mediator, IMapper mapper, IRepository<Employee> repository, IValidator<EmployeeRequestDto> validator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<EmployeeResponseDto>> CreateEmployee(EmployeeRequestDto employeeRequest)
        {
            await _validator.ValidateAndThrowAsync(employeeRequest);

            #region Department

            var departmentResult = await _mediator.Send(new GetDepartmentByNameQuery(employeeRequest.Department.Name));

            if (departmentResult.Status == ResultResponseKind.NotFound)
                departmentResult = await _mediator.Send(new CreateDepartmentCommand(employeeRequest.Department.Name));

            if (!departmentResult.IsSuccess)
                return Result<EmployeeResponseDto>.InternalServerError(departmentResult.ErrorType!.Value, departmentResult.Message, departmentResult.Errors);

            #endregion Department

            #region Create User

            var createUserCommand = _mapper.Map<CreateUserCommand>(employeeRequest.User);
            var userResult = await _mediator.Send(createUserCommand);

            if (!userResult.IsSuccess)
                return Result<EmployeeResponseDto>.InternalServerError(userResult.ErrorType!.Value, userResult.Message, userResult.Errors);

            #endregion Create User

            #region Create Employee

            var createEmployeeCommand = new CreateEmployeeCommand(employeeRequest.HireDate, userResult.Data!.Id, departmentResult.Data!.Id);
            var employeeResult = await _mediator.Send(createEmployeeCommand);

            if (!employeeResult.IsSuccess)
                return Result<EmployeeResponseDto>.InternalServerError(employeeResult.ErrorType!.Value, employeeResult.Message, employeeResult.Errors);

            #endregion Create Employee

            await _repository.SaveAsync();
            return Result<EmployeeResponseDto>.Persisted(employeeResult.Data!, string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(Employee)));
        }
    }
}