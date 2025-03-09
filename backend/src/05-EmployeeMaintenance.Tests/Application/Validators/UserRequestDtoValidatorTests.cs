using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Validators.Users;
using EmployeeMaintenance.Tests.Builders.DTOs.Response;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace EmployeeMaintenance.Tests.Application.Validators.Users
{
    public class UserRequestDtoValidatorTests
    {
        private readonly UserRequestDtoValidator _validator;
        private readonly Mock<IValidator<AddressRequestDto>> _mockAddressValidator;

        public UserRequestDtoValidatorTests()
        {
            _mockAddressValidator = new Mock<IValidator<AddressRequestDto>>();
            _validator = new UserRequestDtoValidator(_mockAddressValidator.Object);
        }

        [Fact]
        public void Validator_ShouldPass_WhenUserRequestIsValid()
        {
            // Arrange
            var request = new UserRequestDtoBuilder().Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenFirstNameIsEmpty(string invalidFirstName)
        {
            // Arrange
            var request = new UserRequestDtoBuilder()
                .WithFirstName(invalidFirstName)
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UserRequestDto.FirstName));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenLastNameIsEmpty(string invalidLastName)
        {
            // Arrange
            var request = new UserRequestDtoBuilder()
                .WithLastName(invalidLastName)
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UserRequestDto.LastName));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }

        [Fact]
        public void Validator_ShouldFail_WhenFirstNameLengthIsInvalid()
        {
            // Arrange
            var request = new UserRequestDtoBuilder()
                .WithFirstName(new string('a', 101))
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UserRequestDto.FirstName));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldMustHaveLengthBetweenCode);
        }

        [Fact]
        public void Validator_ShouldFail_WhenLastNameLengthIsInvalid()
        {
            // Arrange
            var request = new UserRequestDtoBuilder()
                .WithLastName(new string('a', 101))
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UserRequestDto.LastName));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldMustHaveLengthBetweenCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenPhoneIsEmpty(string invalidPhone)
        {
            // Arrange
            var request = new UserRequestDtoBuilder().WithPhone(invalidPhone).Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UserRequestDto.Phone));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }

        [Fact]
        public void Validator_ShouldFail_WhenPhoneLengthIsInvalid()
        {
            // Arrange
            var request = new UserRequestDtoBuilder()
                .WithPhone(new string('1', 21))
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(UserRequestDto.Phone));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldMustHaveLengthBetweenCode);
        }
    }
}