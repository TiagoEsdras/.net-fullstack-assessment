using Bogus;
using EmployeeMaintenance.Domain.Entities;
using System.Reflection;

namespace EmployeeMaintenance.Tests.Builders.Entities
{
    public class AddressBuilder
    {
        private readonly Faker _faker = new();
        private readonly Address _instance;

        public AddressBuilder()
        {
            _instance = (Address)Activator.CreateInstance(typeof(Address), nonPublic: true)!;

            WithStreet(_faker.Address.StreetAddress())
            .WithCity(_faker.Address.City())
            .WithState(_faker.Address.State())
            .WithZipCode(_faker.Address.ZipCode())
            .WithUserId(Guid.NewGuid());
        }

        public AddressBuilder WithStreet(string street)
        {
            SetPrivateField(nameof(Address.Street), street);
            return this;
        }

        public AddressBuilder WithCity(string city)
        {
            SetPrivateField(nameof(Address.City), city);
            return this;
        }

        public AddressBuilder WithState(string state)
        {
            SetPrivateField(nameof(Address.State), state);
            return this;
        }

        public AddressBuilder WithZipCode(string zipCode)
        {
            SetPrivateField(nameof(Address.ZipCode), zipCode);
            return this;
        }

        public AddressBuilder WithUserId(Guid userId)
        {
            SetPrivateField(nameof(Address.UserId), userId);
            return this;
        }

        public Address Build() => _instance;

        private void SetPrivateField(string fieldName, object value)
        {
            var field = _instance.GetType().GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            field?.SetValue(_instance, value);
        }
    }
}