using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using FluentValidation;

namespace EmployeeMaintenance.Application.Validators.Employees
{
    public class CreateEmployeeRequestDtoValidator : AbstractValidator<EmployeeRequestDto>
    {
        public CreateEmployeeRequestDtoValidator(IValidator<DepartmentRequestDto> departmentRequestDtoValidator, IValidator<UserRequestDto> userRequestDtoValidator)
        {
            RuleFor(x => x.HireDate)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.FieldCannotBeNullOrEmptyCode)
                .WithMessage(string.Format(ErrorMessages.FieldCannotBeNullOrEmpty, nameof(EmployeeRequestDto.HireDate)));

            RuleFor(x => x.Department)
                .SetValidator(departmentRequestDtoValidator);

            RuleFor(x => x.User)
                .SetValidator(userRequestDtoValidator);
        }
    }
}