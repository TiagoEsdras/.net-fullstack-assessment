using AutoMapper;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Employees
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<EmployeeResponseDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Result<EmployeeResponseDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employye = request.ToEntity();
            await _employeeRepository.AddAsync(employye);
            await _employeeRepository.SaveAsync();
            var employeeDto = _mapper.Map<EmployeeResponseDto>(employye);
            return Result<EmployeeResponseDto>.Persisted(employeeDto, string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(Employee)));
        }
    }
}