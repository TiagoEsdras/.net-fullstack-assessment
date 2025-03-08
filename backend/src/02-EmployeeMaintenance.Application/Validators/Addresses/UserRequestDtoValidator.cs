using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using FluentValidation;

namespace EmployeeMaintenance.Application.Validators.Addresses
{
    public class AddressRequestDtoValidator : AbstractValidator<AddressRequestDto>
    {
        public AddressRequestDtoValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(AddressRequestDto.Street)));

            RuleFor(x => x.Street)
               .Length(1, 255)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(AddressRequestDto.Street), 1, 255));

            RuleFor(x => x.City)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(AddressRequestDto.City)));

            RuleFor(x => x.City)
               .Length(1, 100)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(AddressRequestDto.City), 1, 100));

            RuleFor(x => x.State)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(AddressRequestDto.State)));

            RuleFor(x => x.State)
               .Length(1, 100)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(AddressRequestDto.State), 1, 100));

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(AddressRequestDto.ZipCode)));

            RuleFor(x => x.ZipCode)
               .Length(1, 20)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(AddressRequestDto.ZipCode), 1, 20));
        }
    }
}