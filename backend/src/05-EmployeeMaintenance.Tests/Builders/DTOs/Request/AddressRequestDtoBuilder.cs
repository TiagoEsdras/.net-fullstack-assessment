using Bogus;
using EmployeeMaintenance.Application.DTOs.Request;

namespace EmployeeMaintenance.Tests.Builders.DTOs.Request
{
    public class AddressRequestDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly AddressRequestDto _instance;

        public AddressRequestDtoBuilder()
        {
            _instance = new AddressRequestDto
            {
                Street = _faker.Address.StreetAddress(),
                City = _faker.Address.City(),
                State = _faker.Address.State(),
                ZipCode = _faker.Address.ZipCode()
            };
        }

        public AddressRequestDtoBuilder WithStreet(string street)
        {
            _instance.Street = street;
            return this;
        }

        public AddressRequestDtoBuilder WithCity(string city)
        {
            _instance.City = city;
            return this;
        }

        public AddressRequestDtoBuilder WithState(string state)
        {
            _instance.State = state;
            return this;
        }

        public AddressRequestDtoBuilder WithZipCode(string zipCode)
        {
            _instance.ZipCode = zipCode;
            return this;
        }

        public AddressRequestDto Build() => _instance;
    }
}