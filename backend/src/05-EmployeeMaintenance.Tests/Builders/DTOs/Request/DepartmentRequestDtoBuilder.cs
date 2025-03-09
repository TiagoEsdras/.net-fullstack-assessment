using Bogus;
using EmployeeMaintenance.Application.DTOs.Request;

namespace EmployeeMaintenance.Tests.Builders.DTOs.Request
{
    public class DepartmentRequestDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly DepartmentRequestDto _instance;

        public DepartmentRequestDtoBuilder()
        {
            _instance = new DepartmentRequestDto
            {
                DepartmentName = _faker.Commerce.Department()
            };
        }

        public DepartmentRequestDtoBuilder WithDepartmentName(string departmentName)
        {
            _instance.DepartmentName = departmentName;
            return this;
        }

        public DepartmentRequestDto Build() => _instance;
    }
}