using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Validators.Addresses;
using EmployeeMaintenance.Application.Validators.Departments;
using EmployeeMaintenance.Application.Validators.Employees;
using EmployeeMaintenance.Application.Validators.Users;
using FluentValidation;

namespace EmployeeMaintenance.Api.Configurations
{
    public static class ValidatorsConfig
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<EmployeeRequestDto>, CreateEmployeeRequestDtoValidator>();
            services.AddScoped<IValidator<AddressRequestDto>, AddressRequestDtoValidator>();
            services.AddScoped<IValidator<UserRequestDto>, UserRequestDtoValidator>();
            services.AddScoped<IValidator<DepartmentRequestDto>, DepartmentRequestDtoValidator>();
        }
    }
}