using Bogus;
using EmployeeMaintenance.Domain.Entities;
using System.Reflection;

namespace EmployeeMaintenance.Tests.Builders.Entities
{
    public class DepartmentBuilder
    {
        private readonly Faker _faker = new();
        private readonly Department _instance;

        public DepartmentBuilder()
        {
            _instance = (Department)Activator.CreateInstance(typeof(Department), nonPublic: true)!;
            WithName(_faker.Company.CompanyName());
        }

        public DepartmentBuilder WithName(string name)
        {
            SetPrivateField(nameof(Department.Name), name);
            return this;
        }

        public Department Build() => _instance;

        private void SetPrivateField(string fieldName, object value)
        {
            var field = _instance.GetType().GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            field?.SetValue(_instance, value);
        }
    }
}