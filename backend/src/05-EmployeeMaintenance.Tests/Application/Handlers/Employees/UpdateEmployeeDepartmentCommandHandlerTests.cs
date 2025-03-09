using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Handlers.Employees;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.Commands.Employees;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using EmployeeMaintenance.Tests.Builders.Entities;
using FluentAssertions;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Handlers.Employees
{
    public class UpdateEmployeeDepartmentCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateEmployeeDepartmentCommandHandler _handler;

        public UpdateEmployeeDepartmentCommandHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateEmployeeDepartmentCommandHandler(_mockEmployeeRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenEmployeeIsUpdated()
        {
            // Arrange
            var command = new UpdateEmployeeDepartmentCommandBuilder().Build();

            var oldDepartment = new DepartmentBuilder().Build();

            var newDepartment = new DepartmentBuilder()
                .WithName(command.Department.Name)
                .Build();

            var employee = new EmployeeBuilder()
                .WithDepartment(oldDepartment)
                .Build();

            employee.UpdateDepartment(newDepartment);

            var departmentDto = new DepartmentResponseDtoBuilder()
                .WithName(newDepartment.Name)
                .WithId(newDepartment.Id)
                .Build();

            var employeeDto = new EmployeeResponseDtoBuilder()
                .WithDepartment(departmentDto)
                .Build();

            _mockEmployeeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(employee);
            _mockMapper.Setup(m => m.Map<Department>(It.IsAny<DepartmentResponseDto>()))
                .Returns(newDepartment);
            _mockMapper.Setup(m => m.Map<EmployeeResponseDto>(It.IsAny<Employee>()))
                .Returns(employeeDto);
            _mockEmployeeRepository.Setup(r => r.Update(It.IsAny<Employee>()));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEquivalentTo(employeeDto);
            result.Message.Should().Be(string.Format(SuccessMessages.EntityUpdatedWithSuccess, nameof(Employee)));
            _mockEmployeeRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.Update(It.IsAny<Employee>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUpdateFails()
        {
            // Arrange
            var command = new UpdateEmployeeDepartmentCommandBuilder().Build();
            var employee = new EmployeeBuilder().Build();
            var department = new DepartmentBuilder().Build();

            _mockEmployeeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(employee);
            _mockMapper.Setup(m => m.Map<Department>(It.IsAny<DepartmentResponseDto>()))
                .Returns(department);
            _mockEmployeeRepository.Setup(r => r.Update(It.IsAny<Employee>()))
                .Throws(new Exception("Database error"));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
            _mockEmployeeRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.Update(It.IsAny<Employee>()), Times.Once);
            _mockEmployeeRepository.Verify(r => r.SaveAsync(), Times.Never);
        }
    }
}