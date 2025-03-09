using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Handlers.Employees;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Queries.Employees;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using FluentAssertions;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Handlers.Employees
{
    public class GetEmployeeByIdQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetEmployeeByIdQueryHandler _handler;

        public GetEmployeeByIdQueryHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetEmployeeByIdQueryHandler(_mockEmployeeRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenEmployeeIsFound()
        {
            // Arrange
            var query = new GetEmployeeByIdQuery(Guid.NewGuid());

            var employeeDto = new EmployeeResponseDtoBuilder()
                .WithId(query.Id)
                .Build();

            _mockEmployeeRepository.Setup(r => r.GetByIdAsync(query.Id))
                .ReturnsAsync(new Employee());
            _mockMapper.Setup(m => m.Map<EmployeeResponseDto>(It.IsAny<Employee>()))
                .Returns(employeeDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEquivalentTo(employeeDto);
            result.Message.Should().Be(string.Format(SuccessMessages.GetEntityByTermWithSuccess, nameof(Employee), nameof(query.Id)));
            _mockEmployeeRepository.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenEmployeeIsNotFound()
        {
            // Arrange
            var query = new GetEmployeeByIdQuery(Guid.NewGuid());

            _mockEmployeeRepository.Setup(r => r.GetByIdAsync(query.Id))
                .ReturnsAsync((Employee)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            result.ErrorType.Should().Be(ErrorType.DataNotFound);
            result.Message.Should().Be(string.Format(ErrorMessages.NotFoundEntity, nameof(Employee)));
            result.Errors.Should().ContainEquivalentOf(new ErrorMessage(ErrorCodes.NotFoundEntityByTermCode, string.Format(ErrorMessages.NotFoundEntityByTerm, nameof(Employee), nameof(query.Id), query.Id)));
            _mockEmployeeRepository.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
        }
    }
}