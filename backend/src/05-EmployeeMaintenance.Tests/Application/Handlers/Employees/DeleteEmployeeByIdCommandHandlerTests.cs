using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.Handlers.Employees;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Handlers.Employees
{
    public class DeleteEmployeeByIdCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly DeleteEmployeeByIdCommandHandler _handler;

        public DeleteEmployeeByIdCommandHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _handler = new DeleteEmployeeByIdCommandHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenEmployeeIsFoundAndDeleted()
        {
            var employeeId = Guid.NewGuid();
            var command = new DeleteEmployeeByIdCommand(Guid.NewGuid());

            _mockEmployeeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Employee());

            _mockEmployeeRepository.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _mockEmployeeRepository.Setup(r => r.SaveAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeTrue();
            result.Message.Should().Be(string.Format(SuccessMessages.DeleteEntityByTermWithSuccess, nameof(Employee), nameof(command.Id), command.Id));
            _mockEmployeeRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenEmployeeIsNotFound()
        {
            // Arrange
            var command = new DeleteEmployeeByIdCommand(Guid.NewGuid());

            _mockEmployeeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Employee)null!);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            result.Message.Should().Be(string.Format(ErrorMessages.NotFoundEntity, nameof(Employee)));
            _mockEmployeeRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnInternalServerError_WhenDeleteFails()
        {
            // Arrange
            var command = new DeleteEmployeeByIdCommand(Guid.NewGuid());

            _mockEmployeeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Employee());

            _mockEmployeeRepository.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.InternalServerError);
            result.Message.Should().Be(ErrorMessages.InternalServerError);
            _mockEmployeeRepository.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenDeleteFails()
        {
            // Arrange
            var command = new DeleteEmployeeByIdCommand(Guid.NewGuid());

            _mockEmployeeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Employee());
            _mockEmployeeRepository.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
            _mockEmployeeRepository.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.SaveAsync(), Times.Never);
        }
    }
}