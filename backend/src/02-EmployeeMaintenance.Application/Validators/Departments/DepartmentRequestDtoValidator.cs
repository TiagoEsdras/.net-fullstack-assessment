using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using FluentValidation;

namespace EmployeeMaintenance.Application.Validators.Departments
{
    public class DepartmentRequestDtoValidator : AbstractValidator<DepartmentRequestDto>
    {
        public DepartmentRequestDtoValidator()
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(DepartmentRequestDto.DepartmentName)));

            RuleFor(x => x.DepartmentName)
               .Length(1, 100)
               .WithErrorCode(ErrorCodes.FieldMustHaveLengthBetweenCode)
               .WithMessage(string.Format(ErrorMessages.FieldMustHaveLengthBetween, nameof(DepartmentRequestDto.DepartmentName), 1, 100));
        }
    }
}