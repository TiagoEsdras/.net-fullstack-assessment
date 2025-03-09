using Bogus;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;

namespace EmployeeMaintenance.Tests.Builders.Commands.Employees
{
    public class UpdateEmployeeDepartmentCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly UpdateEmployeeDepartmentCommand _instance;

        public UpdateEmployeeDepartmentCommandBuilder()
        {
            _instance = new UpdateEmployeeDepartmentCommand
            {
                EmployeeId = Guid.NewGuid(),
                Department = new DepartmentResponseDtoBuilder().Build()
            };
        }

        public UpdateEmployeeDepartmentCommandBuilder WithEmployeeId(Guid employeeId)
        {
            _instance.EmployeeId = employeeId;
            return this;
        }

        public UpdateEmployeeDepartmentCommandBuilder WithDepartment(DepartmentResponseDto department)
        {
            _instance.Department = department;
            return this;
        }

        public UpdateEmployeeDepartmentCommand Build() => _instance;
    }
}