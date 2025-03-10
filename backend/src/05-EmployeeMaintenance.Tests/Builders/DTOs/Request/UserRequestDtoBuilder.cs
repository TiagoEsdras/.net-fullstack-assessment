using Bogus;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Tests.Builders.DTOs.Request;

namespace EmployeeMaintenance.Tests.Builders.DTOs.Response
{
    public class UserRequestDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly UserRequestDto _instance;

        public UserRequestDtoBuilder()
        {
            _instance = new UserRequestDto
            {
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Phone = _faker.Phone.PhoneNumber(),
                PhotoBase64 = Convert.ToBase64String(new byte[] { 0xFF, 0xD8, 0xFF }),
                Address = new AddressRequestDtoBuilder().Build()
            };
        }

        public UserRequestDtoBuilder WithFirstName(string firstName)
        {
            _instance.FirstName = firstName;
            return this;
        }

        public UserRequestDtoBuilder WithLastName(string lastName)
        {
            _instance.LastName = lastName;
            return this;
        }

        public UserRequestDtoBuilder WithPhone(string phone)
        {
            _instance.Phone = phone;
            return this;
        }

        public UserRequestDtoBuilder WithAddress(AddressRequestDto address)
        {
            _instance.Address = address;
            return this;
        }

        public UserRequestDto Build() => _instance;
    }
}