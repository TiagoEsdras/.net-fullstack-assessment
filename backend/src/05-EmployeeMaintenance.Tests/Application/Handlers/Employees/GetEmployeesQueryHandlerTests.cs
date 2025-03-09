using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Request;
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
    public class GetEmployeesQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetEmployeesQueryHandler _handler;

        public GetEmployeesQueryHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetEmployeesQueryHandler(_mockEmployeeRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoEmployeesExist()
        {
            // Arrange
            var query = new GetEmployeesQuery(new PaginationRequest { PageNumber = 1, PageSize = 10 });

            _mockEmployeeRepository.Setup(r => r.GetAllAsync(query.Pagination.PageNumber, query.Pagination.PageSize))
                .ReturnsAsync(new List<Employee>());
            _mockEmployeeRepository.Setup(r => r.CountAsync())
                .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEmpty();
            result.Message.Should().Be(string.Format(SuccessMessages.GetEntitiesWithSuccess, nameof(Employee)));
            result.PaginationResponse!.TotalItems.Should().Be(0);
            result.PaginationResponse!.TotalPages.Should().Be(0);
            result.PaginationResponse!.CurrentPage.Should().Be(query.Pagination.PageNumber);
            result.PaginationResponse!.PageSize.Should().Be(query.Pagination.PageSize);
            _mockEmployeeRepository.Verify(r => r.GetAllAsync(query.Pagination.PageNumber, query.Pagination.PageSize), Times.Once);
        }

        [Theory]
        [InlineData(2, 5, 13, 3, 5)]
        [InlineData(3, 5, 13, 3, 3)]
        public async Task Handle_ShouldReturnEmployees_WhenEmployeesExist(int pageNumber, int pageSize, int totalItems, int totalPages, int countData)
        {
            // Arrange
            var query = new GetEmployeesQuery(new PaginationRequest { PageNumber = pageNumber, PageSize = pageSize });

            var employeeDtos = new List<EmployeeResponseDto>();
            foreach (var i in Enumerable.Range(1, countData))
                employeeDtos.Add(new EmployeeResponseDtoBuilder().Build());

            _mockEmployeeRepository.Setup(r => r.GetAllAsync(query.Pagination.PageNumber, query.Pagination.PageSize))
                .ReturnsAsync(It.IsAny<IEnumerable<Employee>>());
            _mockEmployeeRepository.Setup(r => r.CountAsync())
                .ReturnsAsync(totalItems);
            _mockMapper.Setup(m => m.Map<IEnumerable<EmployeeResponseDto>>(It.IsAny<IEnumerable<Employee>>()))
                .Returns(employeeDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEquivalentTo(employeeDtos);
            result.Data.Should().HaveCount(countData);
            result.Message.Should().Be(string.Format(SuccessMessages.GetEntitiesWithSuccess, nameof(Employee)));
            result.PaginationResponse!.TotalItems.Should().Be(totalItems);
            result.PaginationResponse!.TotalPages.Should().Be(totalPages);
            result.PaginationResponse!.CurrentPage.Should().Be(pageNumber);
            result.PaginationResponse!.PageSize.Should().Be(pageSize);
            _mockEmployeeRepository.Verify(r => r.GetAllAsync(query.Pagination.PageNumber, query.Pagination.PageSize), Times.Once);
        }
    }
}