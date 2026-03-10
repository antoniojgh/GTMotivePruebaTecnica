using System;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GtMotive.Estimate.Microservice.Api.Filters
{
    /// <summary>
    /// Exception filter that converts domain exceptions into appropriate HTTP responses.
    /// </summary>
    public sealed class BusinessExceptionFilter(IAppLogger<BusinessExceptionFilter> appLogger) : IExceptionFilter
    {
        private readonly IAppLogger<BusinessExceptionFilter> _appLogger = appLogger;

        /// <summary>
        /// Called when an unhandled exception occurs during action execution.
        /// </summary>
        /// <param name="context">The exception context.</param>
        public void OnException(ExceptionContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            _appLogger.LogError(context.Exception, "Exception captured in BusinessExceptionFilter.");

            if (context.Exception is DomainException)
            {
                // Map specific domain exceptions to appropriate HTTP status codes and titles
                var (statusCode, title) = context.Exception switch
                {
                    PersonAlreadyHasActiveRentalException => (StatusCodes.Status409Conflict, "Conflict"),
                    VehicleNotAvailableException => (StatusCodes.Status409Conflict, "Conflict"),
                    RentalAlreadyClosedException => (StatusCodes.Status409Conflict, "Conflict"),
                    VehicleAlreadyAvailableException => (StatusCodes.Status409Conflict, "Conflict"),
                    VehicleManufactureDateTooOldException => (StatusCodes.Status422UnprocessableEntity, "Unprocessable Entity"),
                    VehicleManufactureDateInFutureException => (StatusCodes.Status400BadRequest, "Bad Request"),
                    _ => (StatusCodes.Status400BadRequest, "Bad Request"),
                };

                var problemDetails = new ProblemDetails
                {
                    Type = $"https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Status = statusCode,
                    Title = title,
                    Detail = context.Exception.Message,
                    Instance = context.HttpContext.Request.Path,
                };

                _appLogger.LogWarning("Domain Exception: {status} - {detail}", problemDetails.Status, problemDetails.Detail);

                context.Result = new ObjectResult(problemDetails) { StatusCode = statusCode };
                context.ExceptionHandled = true;
            }
            else
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = context.Exception.Message,
                    Instance = context.HttpContext.Request.Path,
                };

                _appLogger.LogError(context.Exception, "Unhandled Exception");

                context.Result = new InternalServerErrorObjectResult(problemDetails);
                context.ExceptionHandled = true;
            }
        }
    }
}
