using EmployeeMaintenance.Application.Commands.Departments;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Departments
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<Department>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Result<Department>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = request.ToEntity();
            await _departmentRepository.AddAsync(department);
            return Result<Department>.Persisted(department, string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(Department)));
        }
    }
}