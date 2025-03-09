using Bogus;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Tests.Builders.DTOs.Request;

namespace EmployeeMaintenance.Tests.Builders.DTOs.Response
{
    public class EmployeeRequestDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly EmployeeRequestDto _instance;

        public EmployeeRequestDtoBuilder()
        {
            _instance = new EmployeeRequestDto
            {
                HireDate = _faker.Date.Past(),
                Department = new DepartmentRequestDtoBuilder().Build(),
                User = new UserRequestDtoBuilder().Build(),
            };
        }

        public EmployeeRequestDtoBuilder WithHireDate(DateTime hireDate)
        {
            _instance.HireDate = hireDate;
            return this;
        }

        public EmployeeRequestDtoBuilder WithDepartment(DepartmentRequestDto department)
        {
            _instance.Department = department;
            return this;
        }

        public EmployeeRequestDtoBuilder WithUser(UserRequestDto user)
        {
            _instance.User = user;
            return this;
        }

        public EmployeeRequestDto Build() => _instance;
    }
}