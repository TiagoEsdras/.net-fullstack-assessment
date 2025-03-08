using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Shared;
using MediatR;

namespace EmployeeMaintenance.Application.Queries.Departments
{
    public class GetDepartmentsQuery : IRequest<Result<IEnumerable<DepartmentResponseDto>>>
    {
    }
}