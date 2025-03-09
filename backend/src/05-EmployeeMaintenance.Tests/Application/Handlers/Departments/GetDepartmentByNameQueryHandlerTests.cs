using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Handlers.Departments;
using EmployeeMaintenance.Application.Interfaces.Repositories;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Domain.Entities;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using FluentAssertions;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Handlers.Departments
{
    public class GetDepartmentByNameQueryHandlerTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetDepartmentByNameQueryHandler _handler;

        public GetDepartmentByNameQueryHandlerTests()
        {
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetDepartmentByNameQueryHandler(_mockDepartmentRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenDepartmentIsFound()
        {
            // Arrange
            var query = new GetDepartmentByNameQuery("IT");

            var departmentDto = new DepartmentResponseDtoBuilder()
                .WithName(query.Name)
                .Build();

            _mockDepartmentRepository.Setup(r => r.GetDepartmentByNameAsync(query.Name))
                .ReturnsAsync(new Department());
            _mockMapper.Setup(m => m.Map<DepartmentResponseDto>(It.IsAny<Department>()))
                .Returns(departmentDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEquivalentTo(departmentDto);
            result.Message.Should().Be(string.Format(SuccessMessages.GetEntityByTermWithSuccess, nameof(Department), nameof(query.Name)));
            _mockDepartmentRepository.Verify(r => r.GetDepartmentByNameAsync(query.Name), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenDepartmentIsNotFound()
        {
            // Arrange
            var query = new GetDepartmentByNameQuery("IT");

            _mockDepartmentRepository.Setup(r => r.GetDepartmentByNameAsync(query.Name))
                .ReturnsAsync((Department)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            result.ErrorType.Should().Be(ErrorType.DataNotFound);
            result.Message.Should().Be(string.Format(ErrorMessages.NotFoundEntity, nameof(Department)));
            result.Errors.Should().ContainEquivalentOf(new ErrorMessage(ErrorCodes.NotFoundEntityByTermCode, string.Format(ErrorMessages.NotFoundEntityByTerm, nameof(Department), nameof(query.Name), query.Name)));
            _mockDepartmentRepository.Verify(r => r.GetDepartmentByNameAsync(query.Name), Times.Once);
        }
    }
}