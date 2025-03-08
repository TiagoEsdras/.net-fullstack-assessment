using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Departments
{
    public class GetDepartmentByNameQueryHandler : IRequestHandler<GetDepartmentByNameQuery, Result<DepartmentResponseDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public GetDepartmentByNameQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<DepartmentResponseDto>> Handle(GetDepartmentByNameQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetDepartmentByNameAsync(request.Name);
            if (department is null)
                return Result<DepartmentResponseDto>.NotFound(
                    ErrorType.DataNotFound,
                    string.Format(ErrorMessages.NotFoundEntity, nameof(Department)),
                    [new ErrorMessage(ErrorCodes.NotFoundEntityByTermCode, string.Format(ErrorMessages.NotFoundEntityByTerm, nameof(Department), nameof(request.Name), request.Name))]
                );

            var departmentDto = _mapper.Map<DepartmentResponseDto>(department);
            return Result<DepartmentResponseDto>.Success(departmentDto, string.Format(SuccessMessages.GetEntityByTermWithSuccess, nameof(Department), nameof(request.Name)));
        }
    }
}