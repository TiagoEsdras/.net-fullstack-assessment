using Bogus;
using EmployeeMaintenance.Application.DTOs.Response;

namespace EmployeeMaintenance.Tests.Builders.DTOs.Response
{
    public class AddressResponseDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly AddressResponseDto _instance;

        public AddressResponseDtoBuilder()
        {
            _instance = new AddressResponseDto
            {
                Id = Guid.NewGuid(),
                Street = _faker.Address.StreetAddress(),
                City = _faker.Address.City(),
                State = _faker.Address.State(),
                ZipCode = _faker.Address.ZipCode()
            };
        }

        public AddressResponseDtoBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public AddressResponseDtoBuilder WithStreet(string street)
        {
            _instance.Street = street;
            return this;
        }

        public AddressResponseDtoBuilder WithCity(string city)
        {
            _instance.City = city;
            return this;
        }

        public AddressResponseDtoBuilder WithState(string state)
        {
            _instance.State = state;
            return this;
        }

        public AddressResponseDtoBuilder WithZipCode(string zipCode)
        {
            _instance.ZipCode = zipCode;
            return this;
        }

        public AddressResponseDto Build() => _instance;
    }
}