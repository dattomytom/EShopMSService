using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBLocks.Exceptions.Handlers
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message:{exceptionMessage}, Time of occurrence", exception.Message, DateTime.UtcNow);

            (string Detail, string Title, int StatusCode) detail = exception switch
            {
                InternalServerException =>
                (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException =>
                (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundException =>
                (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                _ =>
                (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };
            var problemDetail = new ProblemDetails
            {
                Title = detail.Title,
                Detail = detail.Detail,
                Status = detail.StatusCode,
                Instance = context.Request.Path
            };
            problemDetail.Extensions.Add("TraceId", context.TraceIdentifier);
            if(exception is ValidationException validationException)
            {
                problemDetail.Extensions.Add("ValidationError", validationException.Errors);
            }
            await context.Response.WriteAsJsonAsync( problemDetail,cancellationToken );
            return true;
        }
    }
}
