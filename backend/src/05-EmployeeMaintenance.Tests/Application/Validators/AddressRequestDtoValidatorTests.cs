using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Validators.Addresses;
using EmployeeMaintenance.Tests.Builders.DTOs.Request;
using FluentAssertions;

namespace EmployeeMaintenance.Tests.Application.Validators
{
    public class AddressRequestDtoValidatorTests
    {
        private readonly AddressRequestDtoValidator _validator;

        public AddressRequestDtoValidatorTests()
        {
            _validator = new AddressRequestDtoValidator();
        }

        [Fact]
        public void Validator_ShouldPass_WhenAddressRequestIsValid()
        {
            // Arrange
            var request = new AddressRequestDtoBuilder().Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenStreetIsEmpty(string invalidStreet)
        {
            // Arrange
            var request = new AddressRequestDtoBuilder()
                .WithStreet(invalidStreet)
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddressRequestDto.Street));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }

        [Fact]
        public void Validator_ShouldFail_WhenStreetLengthIsInvalid()
        {
            // Arrange
            var request = new AddressRequestDtoBuilder()
                .WithStreet(new string('a', 256))
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddressRequestDto.Street));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldMustHaveLengthBetweenCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenCityIsEmpty(string invalidCity)
        {
            // Arrange
            var request = new AddressRequestDtoBuilder()
                .WithCity(invalidCity)
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddressRequestDto.City));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }

        [Fact]
        public void Validator_ShouldFail_WhenCityLengthIsInvalid()
        {
            // Arrange
            var request = new AddressRequestDtoBuilder()
                .WithCity(new string('a', 101))
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddressRequestDto.City));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldMustHaveLengthBetweenCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenStateIsEmpty(string invalidState)
        {
            // Arrange
            var request = new AddressRequestDtoBuilder()
                .WithState(invalidState)
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddressRequestDto.State));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }

        [Fact]
        public void Validator_ShouldFail_WhenStateLengthIsInvalid()
        {
            // Arrange
            var request = new AddressRequestDtoBuilder()
                .WithState(new string('a', 101))
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddressRequestDto.State));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldMustHaveLengthBetweenCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenZipCodeIsEmpty(string invalidZipCode)
        {
            // Arrange
            var request = new AddressRequestDtoBuilder()
                .WithZipCode(invalidZipCode)
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddressRequestDto.ZipCode));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldCannotBeNullOrEmptyCode);
        }

        [Fact]
        public void Validator_ShouldFail_WhenZipLengthIsInvalid()
        {
            // Arrange
            var request = new AddressRequestDtoBuilder()
                .WithZipCode(new string('a', 21))
                .Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(AddressRequestDto.ZipCode));
            result.Errors.Should().Contain(e => e.ErrorCode == ErrorCodes.FieldMustHaveLengthBetweenCode);
        }
    }
}