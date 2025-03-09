using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.Commands.Departments;
using FluentAssertions;

namespace EmployeeMaintenance.Tests.Application.Commands.Departments
{
    public class CreateDepartmentCommandTests
    {
        [Fact]
        public void ToEntity_ShouldReturnDepartment_WithCorrectProperties()
        {
            // Arrange
            var command = new CreateDepartmentCommandBuilder().Build();

            // Act
            var department = command.ToEntity();

            // Assert
            department.Should().NotBeNull();
            department.Name.Should().Be(command.Name);
            department.Should().BeOfType<Department>();
        }
    }
}