using AutoMapper;
using EmployeeMaintenance.Application.Commands.Departments;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Departments
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<DepartmentResponseDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<DepartmentResponseDto>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = request.ToEntity();
            await _departmentRepository.AddAsync(department);
            var departmentDto = _mapper.Map<DepartmentResponseDto>(department);
            return Result<DepartmentResponseDto>.Persisted(departmentDto, string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(Department)));
        }
    }
}