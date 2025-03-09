using AutoMapper;
using EmployeeMaintenance.Application.DTOs.Request;
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
    public class GetDepartmentsQueryHandlerTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetDepartmentsQueryHandler _handler;

        public GetDepartmentsQueryHandlerTests()
        {
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetDepartmentsQueryHandler(_mockDepartmentRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoDepartmentsExist()
        {
            // Arrange
            var query = new GetDepartmentsQuery(new PaginationRequest { PageNumber = 1, PageSize = 10 });

            _mockDepartmentRepository.Setup(r => r.GetAllAsync(query.Pagination.PageNumber, query.Pagination.PageSize))
                .ReturnsAsync([]);
            _mockDepartmentRepository.Setup(r => r.CountAsync())
                .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEmpty();
            result.Message.Should().Be(string.Format(SuccessMessages.GetEntitiesWithSuccess, nameof(Department)));
            result.PaginationResponse!.TotalItems.Should().Be(0);
            result.PaginationResponse!.TotalPages.Should().Be(0);
            result.PaginationResponse!.CurrentPage.Should().Be(query.Pagination.PageNumber);
            result.PaginationResponse!.PageSize.Should().Be(query.Pagination.PageSize);
            _mockDepartmentRepository.Verify(r => r.GetAllAsync(query.Pagination.PageNumber, query.Pagination.PageSize), Times.Once);
        }

        [Theory]
        [InlineData(2, 5, 13, 3, 5)]
        [InlineData(3, 5, 13, 3, 3)]
        public async Task Handle_ShouldReturnDepartments_WhenDepartmentsExist(int pageNumber, int pageSize, int totalItems, int totalPages, int countData)
        {
            // Arrange
            var query = new GetDepartmentsQuery(new PaginationRequest { PageNumber = pageNumber, PageSize = pageSize });

            var departmentDtos = new List<DepartmentResponseDto>();
            foreach (var i in Enumerable.Range(1, countData))
                departmentDtos.Add(new DepartmentResponseDtoBuilder().Build());

            _mockDepartmentRepository.Setup(r => r.GetAllAsync(query.Pagination.PageNumber, query.Pagination.PageSize))
                .ReturnsAsync(It.IsAny<IEnumerable<Department>>());
            _mockDepartmentRepository.Setup(r => r.CountAsync())
                .ReturnsAsync(totalItems);
            _mockMapper.Setup(m => m.Map<IEnumerable<DepartmentResponseDto>>(It.IsAny<IEnumerable<Department>>()))
                .Returns(departmentDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEquivalentTo(departmentDtos);
            result.Data.Should().HaveCount(countData);
            result.Message.Should().Be(string.Format(SuccessMessages.GetEntitiesWithSuccess, nameof(Department)));
            result.PaginationResponse!.TotalItems.Should().Be(totalItems);
            result.PaginationResponse!.TotalPages.Should().Be(totalPages);
            result.PaginationResponse!.CurrentPage.Should().Be(pageNumber);
            result.PaginationResponse!.PageSize.Should().Be(pageSize);
            _mockDepartmentRepository.Verify(r => r.GetAllAsync(query.Pagination.PageNumber, query.Pagination.PageSize), Times.Once);
        }
    }
}