using Bogus;
using EmployeeMaintenance.Application.DTOs.Response;

namespace EmployeeMaintenance.Tests.Builders.DTOs.Response
{
    public class UserResponseDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly UserResponseDto _instance;

        public UserResponseDtoBuilder()
        {
            _instance = new UserResponseDto
            {
                Id = Guid.NewGuid(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Phone = _faker.Phone.PhoneNumber(),
                Address = new AddressResponseDtoBuilder().Build(),
                AddressId = Guid.NewGuid()
            };
        }

        public UserResponseDtoBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public UserResponseDtoBuilder WithFirstName(string firstName)
        {
            _instance.FirstName = firstName;
            return this;
        }

        public UserResponseDtoBuilder WithLastName(string lastName)
        {
            _instance.LastName = lastName;
            return this;
        }

        public UserResponseDtoBuilder WithPhone(string phone)
        {
            _instance.Phone = phone;
            return this;
        }

        public UserResponseDtoBuilder WithAddress(AddressResponseDto address)
        {
            _instance.Address = address;
            return this;
        }

        public UserResponseDtoBuilder WithAddressId(Guid addressId)
        {
            _instance.AddressId = addressId;
            return this;
        }

        public UserResponseDto Build() => _instance;
    }
}