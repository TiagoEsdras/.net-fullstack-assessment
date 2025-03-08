using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared;
using EmployeeMaintenance.Application.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeMaintenance.Api.Converters
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(Result<T> response);
    }

    public class ActionResultConverter : IActionResultConverter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ActionResultConverter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Convert<T>(Result<T> result)
        {
            if (result == null)
                return BuildError(
                    ResultResponseKind.InternalServerError,
                    ErrorType.UnknownError,
                    ErrorMessages.UnknownError,
                    [new ErrorMessage(ErrorCodes.UnknownErrorCode, ErrorMessages.ContactSupport)]
                );

            if (!result.Errors.Any())
            {
                if (result.Data is null)
                    return BuildSuccessResultWithoutData(result.Status, result.Message!);

                if (result.PaginationResponse is not null)
                    SetPagination(result.PaginationResponse);

                return BuildSuccessResult(result.Data!, result.Status, result.Message!);
            }

            return BuildError(result.Status, result.ErrorType!.Value, result.Message, result.Errors);
        }

        private void SetPagination(PaginationResponse pagination)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var response = _httpContextAccessor.HttpContext.Response;
                response.Headers.TryAdd("X-Total-Count", pagination.TotalItems.ToString());
                response.Headers.TryAdd("X-Total-Pages", pagination.TotalPages.ToString());
                response.Headers.TryAdd("X-Page-Size", pagination.PageSize.ToString());
                response.Headers.TryAdd("X-Current-Page", pagination.CurrentPage.ToString());
            }
        }

        private static ObjectResult BuildSuccessResult(object data, ResultResponseKind status, string message)
        {
            var httpStatus = GetSuccessHttpStatusCode(status);

            return new ObjectResult(new
            {
                Data = data,
                Message = message,
                Status = status.ToString()
            })
            {
                StatusCode = (int)httpStatus
            };
        }

        private static ObjectResult BuildSuccessResultWithoutData(ResultResponseKind status, string message)
        {
            var httpStatus = GetSuccessHttpStatusCode(status);

            return new ObjectResult(new
            {
                Message = message,
                Status = status.ToString()
            })
            {
                StatusCode = (int)httpStatus
            };
        }

        private static ObjectResult BuildError(ResultResponseKind status, ErrorType type, string errorMessage, IEnumerable<ErrorMessage> errors)
        {
            var httpStatus = GetErrorHttpStatusCode(status);

            return new ObjectResult(new
            {
                Type = type.ToString(),
                Error = errorMessage,
                Errors = errors
            })
            {
                StatusCode = (int)httpStatus
            };
        }

        private static HttpStatusCode GetErrorHttpStatusCode(ResultResponseKind status)
        {
            return status switch
            {
                ResultResponseKind.BadRequest => HttpStatusCode.BadRequest,
                ResultResponseKind.NotFound => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            };
        }

        private static HttpStatusCode GetSuccessHttpStatusCode(ResultResponseKind status)
        {
            return status switch
            {
                ResultResponseKind.Success => HttpStatusCode.OK,
                ResultResponseKind.DataPersisted => HttpStatusCode.Created,
                ResultResponseKind.DataAccepted => HttpStatusCode.Accepted,
                _ => HttpStatusCode.OK,
            };
        }
    }
}