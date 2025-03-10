using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Domain.Entities;
using MediatR;

namespace EmployeeMaintenance.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Result<UserResponseDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public UserAddressCommand Address { get; set; }

        public User ToEntity()
            => new(FirstName, LastName, Phone, PhotoUrl, Address.ToEntity());
    }
}