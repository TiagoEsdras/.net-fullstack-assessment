using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Queries.Employees;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Employees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, Result<IEnumerable<EmployeeResponseDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EmployeeResponseDto>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync(request.Pagination.PageNumber, request.Pagination.PageSize);
            var totalItems = await _employeeRepository.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.Pagination.PageSize);
            var employeesDtos = _mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);
            var paginationInfo = PaginationResponse.PaginationInfo(request.Pagination, totalItems, totalPages);
            return Result<IEnumerable<EmployeeResponseDto>>.Success(employeesDtos, string.Format(SuccessMessages.GetEntitiesWithSuccess, nameof(Employee)), paginationInfo);
        }
    }
}