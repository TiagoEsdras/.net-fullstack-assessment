using Bogus;
using EmployeeMaintenance.Application.Commands.Users;

namespace EmployeeMaintenance.Tests.Builders.Commands.Users
{
    public class CreateUserCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly CreateUserCommand _instance;

        public CreateUserCommandBuilder()
        {
            _instance = new CreateUserCommand
            {
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Phone = _faker.Phone.PhoneNumber(),
                Address = new UserAddressCommandBuilder().Build()
            };
        }

        public CreateUserCommandBuilder WithFirstName(string firstName)
        {
            _instance.FirstName = firstName;
            return this;
        }

        public CreateUserCommandBuilder WithLastName(string lastName)
        {
            _instance.LastName = lastName;
            return this;
        }

        public CreateUserCommandBuilder WithPhone(string phone)
        {
            _instance.Phone = phone;
            return this;
        }

        public CreateUserCommandBuilder WithAddress(UserAddressCommand address)
        {
            _instance.Address = address;
            return this;
        }

        public CreateUserCommand Build() => _instance;
    }
}