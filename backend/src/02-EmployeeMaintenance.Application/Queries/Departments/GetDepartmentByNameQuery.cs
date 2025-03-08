using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Shared;
using MediatR;

namespace EmployeeMaintenance.Application.Queries.Departments
{
    public class GetDepartmentByNameQuery : IRequest<Result<DepartmentResponseDto>>
    {
        public string Name { get; set; }

        public GetDepartmentByNameQuery(string name)
        {
            Name = name;
        }
    }
}