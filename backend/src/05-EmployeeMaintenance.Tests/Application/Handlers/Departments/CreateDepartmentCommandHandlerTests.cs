using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Handlers.Departments;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.Commands.Departments;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using EmployeeMaintenance.Tests.Builders.Entities;
using FluentAssertions;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Handlers.Departments
{
    public class CreateDepartmentCommandHandlerTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateDepartmentCommandHandler _handler;

        public CreateDepartmentCommandHandlerTests()
        {
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateDepartmentCommandHandler(_mockDepartmentRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenDepartmentIsCreated()
        {
            // Arrange
            var command = new CreateDepartmentCommandBuilder()
                .Build();

            var departmentEntity = new DepartmentBuilder()
                .WithName(command.Name)
                .Build();
            var departmentDto = new DepartmentResponseDtoBuilder()
                .WithName(command.Name)
                .Build();

            _mockDepartmentRepository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .ReturnsAsync(departmentEntity);
            _mockMapper.Setup(m => m.Map<DepartmentResponseDto>(It.IsAny<Department>()))
                .Returns(departmentDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.DataPersisted);
            result.Data.Should().BeEquivalentTo(departmentDto);
            result.Message.Should().Be(string.Format(SuccessMessages.EntityCreatedWithSuccess, nameof(Department)));
            _mockDepartmentRepository.Verify(r => r.AddAsync(It.IsAny<Department>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAddDepartmentFails()
        {
            // Arrange
            var command = new CreateDepartmentCommandBuilder()
                 .Build();

            _mockDepartmentRepository.Setup(r => r.AddAsync(It.IsAny<Department>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
            _mockDepartmentRepository.Verify(r => r.AddAsync(It.IsAny<Department>()), Times.Once);
        }
    }
}