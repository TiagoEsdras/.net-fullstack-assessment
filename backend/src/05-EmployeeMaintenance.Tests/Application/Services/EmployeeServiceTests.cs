using AutoMapper;
using EmployeeMaintenance.Application.Commands.Departments;
using EmployeeMaintenance.Application.Commands.Employees;
using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.DTOs.Response;
using EmployeeMaintenance.Application.Queries.Departments;
using EmployeeMaintenance.Application.Queries.Employees;
using EmployeeMaintenance.Application.Services;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Application.Validators.Departments;
using EmployeeMaintenance.Application.Validators.Employees;
using EmployeeMaintenance.Tests.Builders.DTOs.Request;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<UserRequestDto>> _userValidatorMock;
        private readonly DepartmentRequestDtoValidator _departmentValidator;
        private readonly CreateEmployeeRequestDtoValidator _createEmployeeValidator;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _userValidatorMock = new Mock<IValidator<UserRequestDto>>();
            _departmentValidator = new DepartmentRequestDtoValidator();
            _createEmployeeValidator = new CreateEmployeeRequestDtoValidator(_departmentValidator, _userValidatorMock.Object);
            _employeeService = new EmployeeService(_mediatorMock.Object, _mapperMock.Object, _createEmployeeValidator, _departmentValidator);
        }

        #region Create Employee

        [Fact]
        public async Task CreateEmployee_ShouldReturnSuccess_WhenDepartmentAlreadyExistsEmployeeIsCreatedSuccessfully()
        {
            // Arrange
            var employeeRequest = new EmployeeRequestDtoBuilder().Build();

            var departmentResponseDto = new DepartmentResponseDtoBuilder().Build();

            var userResponseDto = new UserResponseDtoBuilder().Build();

            var employeeResponseDto = new EmployeeResponseDtoBuilder().Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<string>.Success("image/path", string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<DepartmentResponseDto>.Success(departmentResponseDto, string.Empty));

            _mapperMock.Setup(m => m.Map<CreateUserCommand>(employeeRequest.User))
                       .Returns(new CreateUserCommand());

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<UserResponseDto>.Success(userResponseDto, string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<EmployeeResponseDto>.Success(employeeResponseDto, string.Empty));

            // Act
            var result = await _employeeService.CreateEmployee(employeeRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(ResultResponseKind.DataPersisted);
            _mediatorMock.Verify(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnSuccess_WhenDepartmentNoExistIsCreatedAndEmployeeIsCreatedSuccessfully()
        {
            // Arrange
            var employeeRequest = new EmployeeRequestDtoBuilder().Build();

            var departmentResponseDto = new DepartmentResponseDtoBuilder().Build();

            var userResponseDto = new UserResponseDtoBuilder().Build();

            var employeeResponseDto = new EmployeeResponseDtoBuilder().Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<string>.Success("image/path", string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<DepartmentResponseDto>.NotFound(ErrorType.DataNotFound, string.Empty, [new ErrorMessage(string.Empty, string.Empty)]));

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateDepartmentCommand>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(Result<DepartmentResponseDto>.Persisted(departmentResponseDto, string.Empty));

            _mapperMock.Setup(m => m.Map<CreateUserCommand>(employeeRequest.User))
                       .Returns(new CreateUserCommand());

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<UserResponseDto>.Success(userResponseDto, string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<EmployeeResponseDto>.Success(employeeResponseDto, string.Empty));

            // Act
            var result = await _employeeService.CreateEmployee(employeeRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(ResultResponseKind.DataPersisted);
            _mediatorMock.Verify(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnBadRequest_WhenSaveUserImageFails()
        {
            // Arrange
            var employeeRequest = new EmployeeRequestDtoBuilder().Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<string>.BadRequest(ErrorType.InvalidData, string.Empty, []));

            // Act
            var result = await _employeeService.CreateEmployee(employeeRequest);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.BadRequest);
            _mediatorMock.Verify(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnInternalServerError_WhenDepartmentNoExistOnTryCreateResultIsNoSuccess()
        {
            // Arrange
            var employeeRequest = new EmployeeRequestDtoBuilder().Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<string>.Success("image/path", string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<DepartmentResponseDto>.NotFound(ErrorType.DataNotFound, string.Empty, [new ErrorMessage(string.Empty, string.Empty)]));

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateDepartmentCommand>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(Result<DepartmentResponseDto>.BadRequest(ErrorType.InvalidData, string.Empty, [new ErrorMessage(string.Empty, string.Empty)]));

            // Act
            var result = await _employeeService.CreateEmployee(employeeRequest);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.InternalServerError);
            _mediatorMock.Verify(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnInternalServerError_WhenOnTryCreateUserResultIsNoSuccess()
        {
            // Arrange
            var employeeRequest = new EmployeeRequestDtoBuilder().Build();

            var departmentResponseDto = new DepartmentResponseDtoBuilder().Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<string>.Success("image/path", string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<DepartmentResponseDto>.Success(departmentResponseDto, string.Empty));

            _mapperMock.Setup(m => m.Map<CreateUserCommand>(employeeRequest.User))
                       .Returns(new CreateUserCommand());

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<UserResponseDto>.BadRequest(ErrorType.InvalidOperation, string.Empty, [new ErrorMessage(string.Empty, string.Empty)]));

            // Act
            var result = await _employeeService.CreateEmployee(employeeRequest);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.InternalServerError);
            _mediatorMock.Verify(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnInternalServerError_WhenOnTryCreateEmployeeResultIsNoSuccess()
        {
            // Arrange
            var employeeRequest = new EmployeeRequestDtoBuilder().Build();

            var departmentResponseDto = new DepartmentResponseDtoBuilder().Build();

            var userResponseDto = new UserResponseDtoBuilder().Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<string>.Success("image/path", string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<DepartmentResponseDto>.Success(departmentResponseDto, string.Empty));

            _mapperMock.Setup(m => m.Map<CreateUserCommand>(employeeRequest.User))
                       .Returns(new CreateUserCommand());

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<UserResponseDto>.Success(userResponseDto, string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<EmployeeResponseDto>.BadRequest(ErrorType.InvalidOperation, string.Empty, [new ErrorMessage(string.Empty, string.Empty)]));

            // Act
            var result = await _employeeService.CreateEmployee(employeeRequest);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.InternalServerError);
            _mediatorMock.Verify(m => m.Send(It.IsAny<SaveUserImageCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateEmployee_ShouldThrowException_WhenValidationFails()
        {
            // Arrange
            var createEmployeeRequestDto = new EmployeeRequestDtoBuilder()
                .WithHireDate(default)
                .Build();

            // Act
            Func<Task> act = () => _employeeService.CreateEmployee(createEmployeeRequestDto);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        #endregion Create Employee

        #region Update Employee Department

        [Fact]
        public async Task UpdateEmployeeDepartment_ShouldReturnSuccess_WhenDepartmentNameIsTheSame()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var existingEmployee = new EmployeeResponseDtoBuilder().Build();

            var departmentRequest = new DepartmentRequestDtoBuilder()
                .WithDepartmentName(existingEmployee.Department.Name)
                .Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<EmployeeResponseDto>.Success(existingEmployee, string.Empty));

            // Act
            var result = await _employeeService.UpdateEmployeeDepartment(employeeId, departmentRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(ResultResponseKind.Success);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateEmployeeDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateEmployeeDepartment_ShouldReturnSuccess_WhenDepartmentIsUpdatedSuccessfully()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var existingEmployee = new EmployeeResponseDtoBuilder().Build();

            var departmentRequest = new DepartmentRequestDtoBuilder()
                .WithDepartmentName("NewDepartment")
                .Build();

            var departmentDto = new DepartmentResponseDtoBuilder()
                .WithName(departmentRequest.DepartmentName)
                .Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<EmployeeResponseDto>.Success(existingEmployee, string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<DepartmentResponseDto>.Success(departmentDto, string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEmployeeDepartmentCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<EmployeeResponseDto>.Success(existingEmployee, string.Empty));

            // Act
            var result = await _employeeService.UpdateEmployeeDepartment(employeeId, departmentRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(ResultResponseKind.Success);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateEmployeeDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeeDepartment_ShouldReturnNotFound_WhenEmployeeNotFound()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var departmentRequest = new DepartmentRequestDtoBuilder().Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<EmployeeResponseDto>.NotFound(ErrorType.DataNotFound, string.Empty, [new ErrorMessage(string.Empty, string.Empty)]));

            // Act
            var result = await _employeeService.UpdateEmployeeDepartment(employeeId, departmentRequest);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateEmployeeDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateEmployeeDepartment_ShouldReturnNotFound_WhenDepartmentNotFound()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var departmentRequest = new DepartmentRequestDtoBuilder()
                .WithDepartmentName("NonExistentDepartment")
                .Build();

            var existingEmployee = new EmployeeResponseDtoBuilder().Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<EmployeeResponseDto>.Success(existingEmployee, string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<DepartmentResponseDto>.NotFound(ErrorType.DataNotFound, string.Empty, [new ErrorMessage(string.Empty, string.Empty)]));

            // Act
            var result = await _employeeService.UpdateEmployeeDepartment(employeeId, departmentRequest);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateEmployeeDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateEmployeeDepartment_ShouldReturnInternalServerError_WhenUpdateFails()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var existingEmployee = new EmployeeResponseDtoBuilder().Build();

            var departmentRequest = new DepartmentRequestDtoBuilder()
                .WithDepartmentName("NewDepartment")
                .Build();

            var departmentDto = new DepartmentResponseDtoBuilder()
                .WithName(departmentRequest.DepartmentName)
                .Build();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<EmployeeResponseDto>.Success(existingEmployee, string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<DepartmentResponseDto>.Success(departmentDto, string.Empty));

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEmployeeDepartmentCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<EmployeeResponseDto>.BadRequest(ErrorType.InvalidOperation, string.Empty, [new ErrorMessage(string.Empty, string.Empty)]));

            // Act
            var result = await _employeeService.UpdateEmployeeDepartment(employeeId, departmentRequest);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.InternalServerError);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateEmployeeDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeeDepartment_ShouldThrowException_WhenValidationFails()
        {
            // Arrange
            var createEmployeeRequestDto = new DepartmentRequestDtoBuilder()
                .WithDepartmentName(string.Empty)
                .Build();

            // Act
            Func<Task> act = () => _employeeService.UpdateEmployeeDepartment(Guid.NewGuid(), createEmployeeRequestDto);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetDepartmentByNameQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateEmployeeDepartmentCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        #endregion Update Employee Department
    }
}