using Bogus;
using EmployeeMaintenance.Application.Commands.Employees;

namespace EmployeeMaintenance.Tests.Builders.Commands.Employees
{
    public class CreateEmployeeCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly CreateEmployeeCommand _instance;

        public CreateEmployeeCommandBuilder()
        {
            _instance = new CreateEmployeeCommand
            {
                HireDate = _faker.Date.Past(),
                UserId = Guid.NewGuid(),
                DepartmentId = Guid.NewGuid()
            };
        }

        public CreateEmployeeCommandBuilder WithHireDate(DateTime hireDate)
        {
            _instance.HireDate = hireDate;
            return this;
        }

        public CreateEmployeeCommandBuilder WithUserId(Guid userId)
        {
            _instance.UserId = userId;
            return this;
        }

        public CreateEmployeeCommandBuilder WithDepartmentId(Guid departmentId)
        {
            _instance.DepartmentId = departmentId;
            return this;
        }

        public CreateEmployeeCommand Build() => _instance;
    }
}