using Bogus;
using EmployeeMaintenance.Application.Commands.Users;

namespace EmployeeMaintenance.Tests.Builders.Commands.Users
{
    public class UserAddressCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly UserAddressCommand _instance;

        public UserAddressCommandBuilder()
        {
            _instance = new UserAddressCommand
            {
                Street = _faker.Address.StreetAddress(),
                City = _faker.Address.City(),
                State = _faker.Address.State(),
                ZipCode = _faker.Address.ZipCode()
            };
        }

        public UserAddressCommandBuilder WithStreet(string street)
        {
            _instance.Street = street;
            return this;
        }

        public UserAddressCommandBuilder WithCity(string city)
        {
            _instance.City = city;
            return this;
        }

        public UserAddressCommandBuilder WithState(string state)
        {
            _instance.State = state;
            return this;
        }

        public UserAddressCommandBuilder WithZipCode(string zipCode)
        {
            _instance.ZipCode = zipCode;
            return this;
        }

        public UserAddressCommand Build() => _instance;
    }
}