using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Queries.Departments
{
    public class GetDepartmentByNameQuery : IRequest<Result<Department>>
    {
        public string Name { get; set; }

        public GetDepartmentByNameQuery(string name)
        {
            Name = name;
        }
    }
}