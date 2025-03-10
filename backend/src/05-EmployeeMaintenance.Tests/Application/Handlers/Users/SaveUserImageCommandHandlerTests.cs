using EmployeeMaintenance.Application.Handlers.Users;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using EmployeeMaintenance.Tests.Builders.Commands.Users;
using FluentAssertions;

namespace EmployeeMaintenance.Tests.Application.Handlers.Users
{
    public class SaveUserImageCommandHandlerTests
    {
        private readonly SaveUserImageCommandHandler _handler;

        public SaveUserImageCommandHandlerTests()
        {
            _handler = new SaveUserImageCommandHandler();
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenImageIsSavedSuccessfully()
        {
            // Arrange
            var command = new SaveUserImageCommandBuilder().Build();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().Contain($"images/users/{command.UserName}");
            result.Data.Should().EndWith(".jpg");
            result.Message.Should().Be(SuccessMessages.PhotoSavedWithSuccess);
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenPhotoBase64IsInvalid()
        {
            // Arrange
            var command = new SaveUserImageCommandBuilder()
                .WithPhotoBase64("invalid-base64")
                .Build();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.BadRequest);
            result.Message.Should().Be(string.Format(ErrorMessages.FieldContainInvalidValue, command.PhotoBase64));
            result.Errors.Should().ContainEquivalentOf(new ErrorMessage(ErrorCodes.PhotoBase64InvalidFormatCode, ErrorMessages.PhotoBase64InvalidFormat));
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenPhotoIsNotJpegOrPng()
        {
            // Arrange
            var invalidPhotoBase64 = "data:image/gif;base64,R0lGODlhAQABAIAAAAUEBA==";
            var command = new SaveUserImageCommandBuilder()
                .WithPhotoBase64(invalidPhotoBase64)
                .Build();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.BadRequest);
            result.Message.Should().Be(string.Format(ErrorMessages.FieldContainInvalidValue, command.PhotoBase64));
            result.Errors.Should().ContainEquivalentOf(new ErrorMessage(ErrorCodes.PhotoBase64InvalidFormatCode, ErrorMessages.PhotoBase64InvalidFormat));
        }
    }
}