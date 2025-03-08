using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Departments
{
    public class GetDepartmentByNameQueryHandler : IRequestHandler<GetDepartmentByNameQuery, Result<Department>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentByNameQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Result<Department>> Handle(GetDepartmentByNameQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetDepartmentByNameAsync(request.Name);
            if (department is null)
                return Result<Department>.NotFound(
                    ErrorType.DataNotFound,
                    string.Format(ErrorMessages.NotFoundEntity, nameof(Department)),
                    [new ErrorMessage(ErrorCodes.NotFoundEntityByTermCode, string.Format(ErrorMessages.NotFoundEntityByTerm, nameof(Department), nameof(request.Name), request.Name))]
                );

            return Result<Department>.Success(department, string.Format(SuccessMessages.GetEntityByTermWithSuccess, nameof(Department), nameof(request.Name)));
        }
    }
}