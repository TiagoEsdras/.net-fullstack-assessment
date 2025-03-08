using EmployeeMaintenance.Application.Shared;
using MediatR;

namespace EmployeeMaintenance.Application.Commands.Employees
{
    public class DeleteEmployeeByIdCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }

        public DeleteEmployeeByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}