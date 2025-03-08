using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Result<User>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public UserAddressCommand Address { get; set; }

        public User ToEntity()
            => new(FirstName, LastName, Phone, Address.ToEntity());
    }
}