using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using FluentValidation;

namespace EmployeeMaintenance.Application.Validators.Users
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidator(IValidator<AddressRequestDto> addressRequestDtoValidator)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(UserRequestDto.FirstName)));

            RuleFor(x => x.FirstName)
               .Length(1, 100)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(UserRequestDto.FirstName), 1, 100));

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(UserRequestDto.LastName)));

            RuleFor(x => x.LastName)
               .Length(1, 100)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(UserRequestDto.LastName), 1, 100));

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(UserRequestDto.Phone)));

            RuleFor(x => x.FirstName)
               .Length(1, 20)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(UserRequestDto.Phone), 1, 100));

            RuleFor(x => x.Address)
               .SetValidator(addressRequestDtoValidator);
        }
    }
}