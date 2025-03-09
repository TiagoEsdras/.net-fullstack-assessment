using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Handlers.Employees;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.Commands.Employees;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using FluentAssertions;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Handlers.Employees
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateEmployeeCommandHandler _handler;

        public CreateEmployeeCommandHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateEmployeeCommandHandler(_mockEmployeeRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenEmployeeIsCreated()
        {
            // Arrange
            var command = new CreateEmployeeCommandBuilder().Build();
            var departmentDto = new DepartmentResponseDtoBuilder()
                .WithId(command.DepartmentId)
                .Build();
            var userDto = new UserResponseDtoBuilder()
                .WithId(command.UserId)
                .Build();
            var employeeDto = new EmployeeResponseDtoBuilder()
                .WithHireDate(command.HireDate)
                .WithDepartment(departmentDto)
                .WithUser(userDto)
                .Build();

            _mockEmployeeRepository.Setup(r => r.AddAsync(It.IsAny<Employee>()))
                .ReturnsAsync(It.IsAny<Employee>());
            _mockEmployeeRepository.Setup(r => r.SaveAsync())
                .Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<EmployeeResponseDto>(It.IsAny<Employee>()))
                .Returns(employeeDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.DataPersisted);
            result.Data.Should().BeEquivalentTo(employeeDto);
            result.Message.Should().Be(string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(Employee)));
            _mockEmployeeRepository.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.SaveAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<EmployeeResponseDto>(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeCreationFails()
        {
            // Arrange
            var command = new CreateEmployeeCommandBuilder().Build();

            _mockEmployeeRepository.Setup(r => r.AddAsync(It.IsAny<Employee>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
            _mockEmployeeRepository.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.SaveAsync(), Times.Never);
        }
    }
}