using Bogus;
using EmployeeMaintenance.Domain.Entities;
using System.Reflection;

namespace EmployeeMaintenance.Tests.Builders.Entities
{
    public class UserBuilder
    {
        private readonly Faker _faker = new();
        private readonly User _instance;

        public UserBuilder()
        {
            _instance = (User)Activator.CreateInstance(typeof(User), nonPublic: true)!;

            WithFirstName(_faker.Name.FirstName())
            .WithLastName(_faker.Name.LastName())
            .WithPhone(_faker.Phone.PhoneNumber())
            .WithAddress(new AddressBuilder().Build());
        }

        public UserBuilder WithFirstName(string firstName)
        {
            SetPrivateField(nameof(User.FirstName), firstName);
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            SetPrivateField(nameof(User.LastName), lastName);
            return this;
        }

        public UserBuilder WithPhone(string phone)
        {
            SetPrivateField(nameof(User.Phone), phone);
            return this;
        }

        public UserBuilder WithAddress(Address address)
        {
            SetPrivateField(nameof(User.Address), address);
            return this;
        }

        public User Build() => _instance;

        private void SetPrivateField(string fieldName, object value)
        {
            var field = _instance.GetType().GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            field?.SetValue(_instance, value);
        }
    }
}