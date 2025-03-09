using AutoMapper;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Employees
{
    public class UpdateEmployeeDepartmentCommandHandler : IRequestHandler<UpdateEmployeeDepartmentCommand, Result<EmployeeResponseDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeDepartmentCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Result<EmployeeResponseDto>> Handle(UpdateEmployeeDepartmentCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            var newDepartment = _mapper.Map<Department>(request.Department);
            employee!.UpdateDepartment(newDepartment);
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();
            var employeeUpdated = _mapper.Map<EmployeeResponseDto>(employee);
            return Result<EmployeeResponseDto>.Success(employeeUpdated, string.Format(SuccessMessages.EntityUpdatedWithSuccess, nameof(Employee)));
        }
    }
}