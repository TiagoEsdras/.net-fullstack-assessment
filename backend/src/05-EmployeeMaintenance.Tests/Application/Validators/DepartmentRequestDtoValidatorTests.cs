using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Validators.Departments;
using EmployeeMaintenance.Tests.Builders.DTOs.Request;
using FluentAssertions;

namespace EmployeeMaintenance.Tests.Application.Validators.Departments
{
    public class DepartmentRequestDtoValidatorTests
    {
        private readonly DepartmentRequestDtoValidator _validator;

        public DepartmentRequestDtoValidatorTests()
        {
            _validator = new DepartmentRequestDtoValidator();
        }

        [Fact]
        public void Validator_ShouldPass_WhenDepartmentRequestIsValid()
        {
            // Arrange
            var request = new DepartmentRequestDtoBuilder().Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenDepartmentNameIsEmpty(string invalidDepartmentName)
        {
            // Arrange
            var request = new DepartmentRequestDtoBuilder().WithDepartmentName(invalidDepartmentName).Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(DepartmentRequestDto.DepartmentName));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }

        [Fact]
        public void Validator_ShouldFail_WhenDepartmentNameLengthIsInvalid()
        {
            // Arrange
            var request = new DepartmentRequestDtoBuilder()
                .WithDepartmentName(new string('a', 101))
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(DepartmentRequestDto.DepartmentName));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldMustHaveLengthBetweenCode);
        }
    }
}