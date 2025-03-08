using EmployeeMaintenance.Domain.Entities;

namespace EmployeeMaintenance.Application.Commands.Users
{
    public class UserAddressCommand
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public Address ToEntity()
            => new(Street, City, State, ZipCode);
    }
}