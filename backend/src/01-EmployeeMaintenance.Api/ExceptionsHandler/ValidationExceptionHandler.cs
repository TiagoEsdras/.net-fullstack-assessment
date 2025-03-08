using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace EmployeeMaintenance.Api.ExceptionsHandler
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException validationException)
                return false;

            var errorResponse = new
            {
                ErrorType = ErrorType.InvalidData.ToString(),
                ErrorMessage = string.Format(ErrorMessages.FieldContainInvalidValue, string.Join(", ", validationException.Errors.Select(it => it.PropertyName))),
                Errors = validationException.Errors.Select(it => new ErrorMessage(it.ErrorCode, it.ErrorMessage))
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}