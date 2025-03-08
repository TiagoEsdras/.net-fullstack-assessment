using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Queries.Employees;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Employees
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeResponseDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Result<EmployeeResponseDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee is null)
                return Result<EmployeeResponseDto>.NotFound(
                    ErrorType.DataNotFound,
                    string.Format(ErrorMessages.NotFoundEntity, nameof(Employee)),
                    [new(ErrorCodes.NotFoundEntityByTermCode, string.Format(ErrorMessages.NotFoundEntityByTerm, nameof(Employee), nameof(request.Id), request.Id))]
                );

            var employeeDto = _mapper.Map<EmployeeResponseDto>(employee);
            return Result<EmployeeResponseDto>.Success(employeeDto, string.Format(SuccessMessages.GetEntityByTermWithSuccess, nameof(Employee), nameof(request.Id)));
        }
    }
}