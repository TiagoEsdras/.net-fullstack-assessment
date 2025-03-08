using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Commands.Departments
{
    public class CreateDepartmentCommand : IRequest<Result<Department>>
    {
        public string Name { get; set; }

        public Department ToEntity()
            => new(Name);
    }
}