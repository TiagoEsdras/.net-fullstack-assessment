﻿using EmployeeMaintenance.Application.DTOs.Request;
using EmployeeMaintenance.Application.Shared.Enums;
using System.Text.Json.Serialization;

namespace EmployeeMaintenance.Application.Shared
{
    public class Result<T>
    {
        public T? Data { get; private set; }
        public string Message { get; private set; }
        public ErrorType? ErrorType { get; private set; }
        public IEnumerable<ErrorMessage> Errors { get; private set; } = [];
        public ResultResponseKind Status { get; private set; }
        public bool IsSuccess { get; private set; }

        [JsonIgnore]
        public PaginationResponse? PaginationResponse { get; private set; }

        private Result(ResultResponseKind status, T data, string message, PaginationResponse? paginationResponse = null)
        {
            Data = data;
            Message = message;
            Status = status;
            IsSuccess = true;
            PaginationResponse = paginationResponse;
        }

        private Result(ResultResponseKind status, ErrorType errorType, string errorMessage, IEnumerable<ErrorMessage> errors)
        {
            Status = status;
            ErrorType = errorType;
            Message = errorMessage;
            Errors = errors;
            IsSuccess = false;
        }

        public static Result<T> Success(T data, string message) => new(ResultResponseKind.Success, data, message);

        public static Result<T> Success(T data, string message, PaginationResponse pagination) => new(ResultResponseKind.Success, data, message, pagination);

        public static Result<T> Persisted(T data, string message) => new(ResultResponseKind.DataPersisted, data, message);

        public static Result<T> BadRequest(ErrorType errorType, string errorMessage, IEnumerable<ErrorMessage> errors) => new(ResultResponseKind.BadRequest, errorType, errorMessage, errors);

        public static Result<T> NotFound(ErrorType errorType, string errorMessage, IEnumerable<ErrorMessage> errors) => new(ResultResponseKind.NotFound, errorType, errorMessage, errors);

        public static Result<T> InternalServerError(ErrorType errorType, string errorMessage, IEnumerable<ErrorMessage> errors) => new(ResultResponseKind.InternalServerError, errorType, errorMessage, errors);
    }
}