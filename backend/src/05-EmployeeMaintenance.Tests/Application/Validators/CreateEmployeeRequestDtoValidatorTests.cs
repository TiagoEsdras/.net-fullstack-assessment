using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Validators.Employees;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Validators.Employees
{
    public class CreateEmployeeRequestDtoValidatorTests
    {
        private readonly CreateEmployeeRequestDtoValidator _validator;
        private readonly Mock<IValidator<DepartmentRequestDto>> _mockDepartmentValidator;
        private readonly Mock<IValidator<UserRequestDto>> _mockUserValidator;

        public CreateEmployeeRequestDtoValidatorTests()
        {
            _mockDepartmentValidator = new Mock<IValidator<DepartmentRequestDto>>();
            _mockUserValidator = new Mock<IValidator<UserRequestDto>>();
            _validator = new CreateEmployeeRequestDtoValidator(_mockDepartmentValidator.Object, _mockUserValidator.Object);
        }

        [Fact]
        public void Validator_ShouldPass_WhenEmployeeRequestIsValid()
        {
            // Arrange
            var request = new EmployeeRequestDtoBuilder().Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validator_ShouldFail_WhenHireDateIsEmpty()
        {
            // Arrange
            var request = new EmployeeRequestDtoBuilder()
                .WithHireDate(default)
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(EmployeeRequestDto.HireDate));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }
    }
}