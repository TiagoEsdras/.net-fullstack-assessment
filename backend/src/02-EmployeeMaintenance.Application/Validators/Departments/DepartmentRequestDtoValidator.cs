using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using FluentValidation;

namespace EmployeeMaintenance.Application.Validators.Departments
{
    public class DepartmentRequestDtoValidator : AbstractValidator<DepartmentRequestDto>
    {
        public DepartmentRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(DepartmentRequestDto.Name)));

            RuleFor(x => x.Name)
               .Length(1, 100)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(DepartmentRequestDto.Name), 1, 100));
        }
    }
}