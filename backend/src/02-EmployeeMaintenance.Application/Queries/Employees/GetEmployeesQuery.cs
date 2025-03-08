using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Shared;
using MediatR;

namespace EmployeeMaintenance.Application.Queries.Employees
{
    public class GetEmployeesQuery : IRequest<Result<IEnumerable<EmployeeResponseDto>>>
    {
        public GetEmployeesQuery(PaginationRequest paginationRequest)
        {
            Pagination = paginationRequest;
        }

        public PaginationRequest Pagination { get; set; }
    }
}