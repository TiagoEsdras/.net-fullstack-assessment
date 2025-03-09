using Bogus;
using EmployeeMaintenance.Application.DTOs.Response;

namespace EmployeeMaintenance.Tests.Builders.DTOs.Response
{
    public class EmployeeResponseDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly EmployeeResponseDto _instance;

        public EmployeeResponseDtoBuilder()
        {
            _instance = new EmployeeResponseDto
            {
                Id = Guid.NewGuid(),
                HireDate = _faker.Date.Past(),
                Department = new DepartmentResponseDtoBuilder().Build(),
                DepartmentId = Guid.NewGuid(),
                User = new UserResponseDtoBuilder().Build(),
                UserId = Guid.NewGuid()
            };
        }

        public EmployeeResponseDtoBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public EmployeeResponseDtoBuilder WithHireDate(DateTime hireDate)
        {
            _instance.HireDate = hireDate;
            return this;
        }

        public EmployeeResponseDtoBuilder WithDepartment(DepartmentResponseDto department)
        {
            _instance.Department = department;
            _instance.DepartmentId = department.Id;
            return this;
        }

        public EmployeeResponseDtoBuilder WithUser(UserResponseDto user)
        {
            _instance.User = user;
            _instance.UserId = user.Id;
            return this;
        }

        public EmployeeResponseDto Build() => _instance;
    }
}