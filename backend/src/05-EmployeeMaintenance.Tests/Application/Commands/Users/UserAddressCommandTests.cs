using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.Commands.Users;
using FluentAssertions;

namespace EmployeeMaintenance.Tests.Application.Commands.Users
{
    public class UserAddressCommandTests
    {
        [Fact]
        public void ToEntity_ShouldReturnAddress_WithCorrectProperties()
        {
            // Arrange
            var addressCommand = new UserAddressCommandBuilder().Build();

            // Act
            var address = addressCommand.ToEntity();

            // Assert
            address.Should().NotBeNull();
            address.Street.Should().Be(addressCommand.Street);
            address.City.Should().Be(addressCommand.City);
            address.State.Should().Be(addressCommand.State);
            address.ZipCode.Should().Be(addressCommand.ZipCode);
            address.Should().BeOfType<Address>();
        }
    }
}