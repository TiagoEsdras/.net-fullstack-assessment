using EmployeeMaintenance.Application.Commands.Users;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using MediatR;

namespace EmployeeMaintenance.Application.Handlers.Users
{
    public class SaveUserImageCommandHandler : IRequestHandler<SaveUserImageCommand, Result<string>>
    {
        private readonly string _baseDirectory = "wwwroot/images/users";

        public SaveUserImageCommandHandler()
        {
        }

        public async Task<Result<string>> Handle(SaveUserImageCommand request, CancellationToken cancellationToken)
        {
            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(request.PhotoBase64);
            }
            catch (FormatException)
            {
                return Result<string>.BadRequest(ErrorType.InvalidData, string.Format(ErrorMessages.FieldContainInvalidValue, request.PhotoBase64),
                    [new ErrorMessage(ErrorCodes.PhotoBase64InvalidFormatCode, ErrorMessages.PhotoBase64InvalidFormat)]);
            }

            string extension = GetImageExtension(imageBytes);
            if (extension is null)
                return Result<string>.BadRequest(ErrorType.InvalidData, string.Format(ErrorMessages.FieldContainInvalidValue, request.PhotoBase64),
                    [new ErrorMessage(ErrorCodes.PhotoBase64MustBeAnPngOrJpegFormatCode, ErrorMessages.PhotoBase64MustBeAnPngOrJpegFormat)]);

            string fileName = $"{request.UserName}_{DateTime.UtcNow:yyyyMMdd_HHmmssfff}{extension}";

            string filePath = Path.Combine(_baseDirectory, fileName);

            if (!Directory.Exists(_baseDirectory))
                Directory.CreateDirectory(_baseDirectory);

            await File.WriteAllBytesAsync(filePath, imageBytes, cancellationToken);

            string imageUrl = Path.Combine($"images/users/{fileName}");

            return Result<string>.Success(imageUrl, SuccessMessages.PhotoSavedWithSuccess);
        }

        private static string GetImageExtension(byte[] imageBytes)
        {
            // Magic number for JPEG: 0xFF 0xD8 0xFF
            // Magic number for PNG: 0x89 0x50 0x4E 0x47
            if (imageBytes.Length > 4)
            {
                if (imageBytes[0] == 0xFF && imageBytes[1] == 0xD8 && imageBytes[2] == 0xFF)
                    return ".jpg";

                if (imageBytes[0] == 0x89 && imageBytes[1] == 0x50 && imageBytes[2] == 0x4E && imageBytes[3] == 0x47)
                    return ".png";
            }

            return null!;
        }
    }
}