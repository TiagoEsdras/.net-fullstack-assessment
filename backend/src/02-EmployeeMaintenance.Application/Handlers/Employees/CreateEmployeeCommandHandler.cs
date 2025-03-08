using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Employees
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<Employee>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employye = request.ToEntity();
            await _employeeRepository.AddAsync(employye);
            return Result<Employee>.Persisted(employye, string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(Employee)));
        }
    }
}