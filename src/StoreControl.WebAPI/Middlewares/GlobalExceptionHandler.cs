using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using StoreControl.Domain.Errors;
using StoreControl.Domain.Exceptions;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace StoreControl.WebAPI.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorResponse = CreateErrorResponse(exception);

            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = (int)errorResponse.StatusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse), cancellationToken);

            return true;
        }

        private static ErrorResponse CreateErrorResponse(Exception ex)
        {
            var errorResponse = ex switch
            {
                ValidationException validationException => HandleValidationException(validationException),
                BadRequestException badRequestException => HandleBadRequestException(badRequestException),
                NotFoundException dataNotFoundException => HandleNotFoundException(dataNotFoundException),
                _ => HandleUnhandledExceptions(ex),
            };

            return errorResponse;
        }

        private static ErrorResponse HandleValidationException(ValidationException validationException)
        {
            return new ErrorResponse()
            {
                Message = validationException.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = validationException.Errors.Select(err => new ErrorEntry()
                {
                    Message = err.ErrorMessage,
                    Error = err.ErrorCode,
                    Property = err.PropertyName,
                }).ToList()
            };
        }

        private static ErrorResponse HandleBadRequestException(BadRequestException badRequestException)
        {
            return new ErrorResponse()
            {
                Message = badRequestException.Message,
                StatusCode = HttpStatusCode.BadRequest,
            };
        }

        private static ErrorResponse HandleNotFoundException(NotFoundException notFoundException)
        {
            return new ErrorResponse()
            {
                Message = notFoundException.Message,
                StatusCode = HttpStatusCode.NotFound,
            };
        }

        private static ErrorResponse HandleUnhandledExceptions(Exception ex)
        {
            return new ErrorResponse()
            {
                Message = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError,
            };
        }
    }
}
