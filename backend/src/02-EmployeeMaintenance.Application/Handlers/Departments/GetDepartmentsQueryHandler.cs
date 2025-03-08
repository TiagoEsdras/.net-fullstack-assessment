using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Departments
{
    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, Result<IEnumerable<DepartmentResponseDto>>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public GetDepartmentsQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DepartmentResponseDto>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentResponseDto>>(departments);
            return Result<IEnumerable<DepartmentResponseDto>>.Success(departmentDtos, string.Format(SuccessMessages.GetEntitiesWithSuccess, nameof(Department)));
        }
    }
}