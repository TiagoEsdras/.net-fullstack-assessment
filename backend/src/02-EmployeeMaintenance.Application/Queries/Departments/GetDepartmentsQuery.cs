using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Shared;
using MediatR;

namespace EmployeeMaintenance.Application.Queries.Departments
{
    public class GetDepartmentsQuery : IRequest<Result<IEnumerable<DepartmentResponseDto>>>
    {
        public GetDepartmentsQuery(PaginationRequest paginationRequest)
        {
            Pagination = paginationRequest;
        }

        public PaginationRequest Pagination { get; set; }
    }
}