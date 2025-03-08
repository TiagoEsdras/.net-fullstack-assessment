using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using Microsoft.AspNetCore.Diagnostics;

namespace EmployeeMaintenance.Api.ExceptionsHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorResponse = new
            {
                Type = ErrorType.UnknownError.ToString(),
                Error = ErrorMessages.InternalServerError,
                Errors = new List<ErrorMessage>() { new(ErrorCodes.InternalServerErrorCode, exception.Message) }
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}