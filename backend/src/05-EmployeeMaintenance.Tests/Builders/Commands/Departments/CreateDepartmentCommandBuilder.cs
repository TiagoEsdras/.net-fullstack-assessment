using Bogus;
using EmployeeMaintenance.Application.Commands.Departments;

namespace EmployeeMaintenance.Tests.Builders.Commands.Departments
{
    public class CreateDepartmentCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly CreateDepartmentCommand _instance;

        public CreateDepartmentCommandBuilder()
        {
            _instance = new CreateDepartmentCommand(_faker.Commerce.Department());
        }

        public CreateDepartmentCommandBuilder WithName(string name)
        {
            _instance.Name = name;
            return this;
        }

        public CreateDepartmentCommand Build() => _instance;
    }
}