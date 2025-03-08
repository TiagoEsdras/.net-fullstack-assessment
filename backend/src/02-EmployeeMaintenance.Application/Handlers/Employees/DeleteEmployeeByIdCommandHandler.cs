using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Employees
{
    public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, Result<bool>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeByIdCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<bool>> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);

            if (employee is null)
                return Result<bool>.NotFound(
                    ErrorType.DataNotFound,
                    string.Format(ErrorMessages.NotFoundEntity, nameof(Employee)),
                    [new ErrorMessage(ErrorCodes.NotFoundEntityByTermCode, string.Format(ErrorMessages.NotFoundEntityByTerm, nameof(Employee), nameof(request.Id), request.Id))]
                );

            var deleted = await _employeeRepository.DeleteAsync(request.Id);

            if (!deleted)
                return Result<bool>.InternalServerError(
                    ErrorType.UnknownError,
                    ErrorMessages.InternalServerError,
                    [new ErrorMessage(ErrorCodes.InternalServerErrorCode, string.Format(ErrorMessages.AnErrorOccurOnProcessRequest, "Delete Employee", request.Id))]
                );

            await _employeeRepository.SaveAsync();
            return Result<bool>.Success(deleted, string.Format(SuccessMessages.DeleteEntityByTermWithSuccess, nameof(Employee), nameof(request.Id), request.Id));
        }
    }
}