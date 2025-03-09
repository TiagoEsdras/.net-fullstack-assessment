using Bogus;
using EmployeeMaintenance.Application.DTOs.Response;

namespace EmployeeMaintenance.Tests.Builders.DTOs.Response
{
    public class DepartmentResponseDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly DepartmentResponseDto _instance;

        public DepartmentResponseDtoBuilder()
        {
            _instance = new DepartmentResponseDto
            {
                Id = Guid.NewGuid(),
                Name = _faker.Commerce.Department()
            };
        }

        public DepartmentResponseDtoBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public DepartmentResponseDtoBuilder WithName(string name)
        {
            _instance.Name = name;
            return this;
        }

        public DepartmentResponseDto Build() => _instance;
    }
}