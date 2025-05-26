using System.Text.Json;
using FluentValidation;
using RelayController.API.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.API.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (DomainForbiddenAccessException ex)
            {
                await HandleDomainExceptionAsync(context, ex, GetStatusCodeForDomainException(ex));
            }
            catch (DomainException ex)
            {
                await HandleDomainExceptionAsync(context, ex, GetStatusCodeForDomainException(ex));
            }
            catch (Exception ex)
            {
                await HandleDomainExceptionAsync(context, ex, StatusCodes.Status500InternalServerError);
            }
        }
        
        private static int GetStatusCodeForDomainException(DomainException ex)
        {
            return ex switch
            {
                DomainValidationException => StatusCodes.Status400BadRequest,
                DomainRuleViolationException => StatusCodes.Status400BadRequest,

                DomainUnauthorizedAccessException => StatusCodes.Status401Unauthorized,

                DomainForbiddenAccessException => StatusCodes.Status403Forbidden,

                DomainNotFoundException => StatusCodes.Status404NotFound,

                DomainConflictException => StatusCodes.Status409Conflict,

                _ => StatusCodes.Status400BadRequest
            };
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ApiResponse
            {
                Success = false,
                Message = "Validation Failed",
                Errors = exception.Errors
                    .Select(error => (ValidationErrorDetail)error)
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
        
        private static Task HandleDomainExceptionAsync(HttpContext context, Exception exception, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            Success = false,
            Message = exception.Message
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
        
    }
}
