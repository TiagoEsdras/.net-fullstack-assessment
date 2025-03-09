using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.Commands.Users;
using FluentAssertions;

namespace EmployeeMaintenance.Tests.Application.Commands.Users
{
    public class CreateUserCommandTests
    {
        [Fact]
        public void ToEntity_ShouldReturnUser_WithCorrectProperties()
        {
            // Arrange
            var addressCommand = new UserAddressCommandBuilder().Build();
            var userCommand = new CreateUserCommandBuilder().WithAddress(addressCommand).Build();

            // Act
            var user = userCommand.ToEntity();

            // Assert
            user.Should().NotBeNull();
            user.FirstName.Should().Be(userCommand.FirstName);
            user.LastName.Should().Be(userCommand.LastName);
            user.Phone.Should().Be(userCommand.Phone);
            user.Address.Should().NotBeNull();
            user.Should().BeOfType<User>();
        }
    }
}