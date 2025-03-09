using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.Commands.Employees;
using FluentAssertions;

namespace EmployeeMaintenance.Tests.Application.Commands.Employees
{
    public class CreateEmployeeCommandTests
    {
        [Fact]
        public void ToEntity_ShouldReturnEmployee_WithCorrectProperties()
        {
            // Arrange
            var employeeCommand = new CreateEmployeeCommandBuilder().Build();

            // Act
            var employee = employeeCommand.ToEntity();

            // Assert
            employee.Should().NotBeNull();
            employee.HireDate.Should().Be(employeeCommand.HireDate);
            employee.UserId.Should().Be(employeeCommand.UserId);
            employee.DepartmentId.Should().Be(employeeCommand.DepartmentId);
            employee.Should().BeOfType<Employee>();
        }
    }
}