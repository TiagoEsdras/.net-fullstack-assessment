using Bogus;
using EmployeeMaintenance.Domain.Entities;
using System.Reflection;

namespace EmployeeMaintenance.Tests.Builders.Entities
{
    public class EmployeeBuilder
    {
        private readonly Faker _faker = new();
        private readonly Employee _instance;

        public EmployeeBuilder()
        {
            _instance = (Employee)Activator.CreateInstance(typeof(Employee), nonPublic: true)!;

            WithHireDate(_faker.Date.Past())
            .WithUser(new UserBuilder().Build())
            .WithDepartment(new DepartmentBuilder().Build());
        }

        public EmployeeBuilder WithHireDate(DateTime hireDate)
        {
            SetPrivateField(nameof(Employee.HireDate), hireDate);
            return this;
        }

        public EmployeeBuilder WithUser(User user)
        {
            SetPrivateField(nameof(Employee.User), user);
            SetPrivateField(nameof(Employee.UserId), user.Id);
            return this;
        }

        public EmployeeBuilder WithDepartment(Department department)
        {
            SetPrivateField(nameof(Employee.Department), department);
            SetPrivateField(nameof(Employee.DepartmentId), department.Id);
            return this;
        }

        public Employee Build() => _instance;

        private void SetPrivateField(string fieldName, object value)
        {
            var field = _instance.GetType().GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            field?.SetValue(_instance, value);
        }
    }
}