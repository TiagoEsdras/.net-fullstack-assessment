using AutoMapper;
using EmployeeMaintenance.Application.Commands.Departments;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Services;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Queries.Employees;
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
        private readonly IValidator<EmployeeRequestDto> _employeeRequestValidator;
        private readonly IValidator<DepartmentRequestDto> _departmentRequestValidator;

        public EmployeeService(IMediator mediator, IMapper mapper, IValidator<EmployeeRequestDto> validator, IValidator<DepartmentRequestDto> departmentRequestValidator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _employeeRequestValidator = validator;
            _departmentRequestValidator = departmentRequestValidator;
        }

        public async Task<Result<EmployeeResponseDto>> CreateEmployee(EmployeeRequestDto employeeRequest)
        {
            await _employeeRequestValidator.ValidateAndThrowAsync(employeeRequest);

            #region Department

            var departmentResult = await _mediator.Send(new GetDepartmentByNameQuery(employeeRequest.Department.DepartmentName));

            if (departmentResult.Status == ResultResponseKind.NotFound)
                departmentResult = await _mediator.Send(new CreateDepartmentCommand(employeeRequest.Department.DepartmentName));

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

            return Result<EmployeeResponseDto>.Persisted(employeeResult.Data!, string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(Employee)));
        }

        public async Task<Result<EmployeeResponseDto>> UpdateEmployeeDepartment(Guid employeeId, DepartmentRequestDto departmentRequest)
        {
            await _departmentRequestValidator.ValidateAndThrowAsync(departmentRequest);

            #region Employee

            var employeeResult = await _mediator.Send(new GetEmployeeByIdQuery(employeeId));

            if (employeeResult.Status == ResultResponseKind.NotFound)
                return Result<EmployeeResponseDto>.NotFound(
                     ErrorType.DataNotFound,
                     string.Format(ErrorMessages.NotFoundEntity, nameof(Employee)),
                     [new ErrorMessage(ErrorCodes.NotFoundEntityByTermCode, string.Format(ErrorMessages.NotFoundEntityByTerm, nameof(Employee), nameof(employeeId), employeeId))]
                );

            if (employeeResult.Data!.Department.Name.Equals(departmentRequest.DepartmentName, StringComparison.InvariantCultureIgnoreCase))
                return Result<EmployeeResponseDto>.Success(employeeResult.Data!, string.Format(SuccessMessages.EntityUpdatedWithSuccess, nameof(Employee)));

            #endregion Employee

            #region Department

            var departmentResult = await _mediator.Send(new GetDepartmentByNameQuery(departmentRequest.DepartmentName));

            if (departmentResult.Status == ResultResponseKind.NotFound)
                return Result<EmployeeResponseDto>.NotFound(
                     ErrorType.DataNotFound,
                     string.Format(ErrorMessages.NotFoundEntity, nameof(Department)),
                     [new ErrorMessage(ErrorCodes.NotFoundEntityByTermCode, string.Format(ErrorMessages.NotFoundEntityByTerm, nameof(Department), nameof(departmentRequest.DepartmentName), departmentRequest.DepartmentName))]
                );

            #endregion Department

            #region Update Employee Department

            var updateEmployeeDepartmentCommand = new UpdateEmployeeDepartmentCommand(employeeId, departmentResult.Data!);
            var employeeUpdatedResult = await _mediator.Send(updateEmployeeDepartmentCommand);

            if (!employeeUpdatedResult.IsSuccess)
                return Result<EmployeeResponseDto>.InternalServerError(employeeUpdatedResult.ErrorType!.Value, employeeUpdatedResult.Message, employeeUpdatedResult.Errors);

            #endregion Update Employee Department

            return Result<EmployeeResponseDto>.Success(employeeUpdatedResult.Data!, string.Format(SuccessMessages.EntityUpdatedWithSuccess, nameof(Employee)));
        }
    }
}