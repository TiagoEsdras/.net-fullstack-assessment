using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Handlers.Users;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.Commands.Users;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using FluentAssertions;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Handlers.Users
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateUserCommandHandler(_mockUserRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenUserIsCreated()
        {
            // Arrange
            var command = new CreateUserCommandBuilder().Build();

            var addressDto = new AddressResponseDtoBuilder()
                .WithStreet(command.Address.Street)
                .WithCity(command.Address.City)
                .WithState(command.Address.State)
                .WithZipCode(command.Address.ZipCode)
                .Build();
            var userDto = new UserResponseDtoBuilder()
                .WithFirstName(command.FirstName)
                .WithLastName(command.LastName)
                .WithPhone(command.Phone)
                .WithAddress(addressDto)
                .Build();

            _mockUserRepository.Setup(r => r.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(It.IsAny<User>());
            _mockMapper.Setup(m => m.Map<UserResponseDto>(It.IsAny<User>()))
                .Returns(userDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.DataPersisted);
            result.Data.Should().BeEquivalentTo(userDto);
            result.Message.Should().Be(string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(User)));
            _mockUserRepository.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            _mockMapper.Verify(m => m.Map<UserResponseDto>(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserCreationFails()
        {
            // Arrange
            var command = new CreateUserCommandBuilder().Build();

            _mockUserRepository.Setup(r => r.AddAsync(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
            _mockUserRepository.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}