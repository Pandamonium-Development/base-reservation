using System.ComponentModel.DataAnnotations;
using System.Net;
using BaseReservation.Application.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace BaseReservation.WebAPI.Configuration;

/// <summary>
/// Exception handling configuration extension class
/// </summary>
public static class ExceptionHandlingConfigurationExtension
{
    /// <summary>
    /// Configuration exception handler extension to catch errors
    /// </summary>
    /// <param name="app">Application builder</param>
    /// <param name="logger">App log</param>
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFailure = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFailure != null)
                {
                    var errorDetails = GetErrorDetails(contextFailure);
                    context.Response.StatusCode = errorDetails.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errorDetails.ToString());
                }
            });
        });
    }

    /// <summary>
    /// Get details of the error catched
    /// </summary>
    /// <param name="exception">Exception handler feature</param>
    /// <returns>ErrorDetailsBaseReservation</returns>
    private static ErrorDetailsBaseReservation GetErrorDetails(IExceptionHandlerFeature exception)
    {
        HttpStatusCode httpStatusCode;
        LogLevel logLevel;
        switch (exception.Error)
        {
            case NotFoundException d:
                httpStatusCode = d.HttpStatusCode;
                logLevel = d.LogLevel;
                break;
            case BaseReservationException e:
                httpStatusCode = e.HttpStatusCode;
                logLevel = e.LogLevel;
                break;
            case UnAuthorizedException f:
                httpStatusCode = f.HttpStatusCode;
                logLevel = f.LogLevel;
                break;
            case ValidationException:
            case FluentValidation.ValidationException:
            case ValidationEntityException:
                httpStatusCode = HttpStatusCode.UnprocessableEntity;
                logLevel = LogLevel.Information;
                break;
            case ListNotAddedException g:
                httpStatusCode = g.HttpStatusCode;
                logLevel = g.LogLevel;
                break;
            case BadRequestException:
                httpStatusCode = HttpStatusCode.BadRequest;
                logLevel = LogLevel.Information;
                break;
            default:
                httpStatusCode = HttpStatusCode.MethodNotAllowed;
                logLevel = LogLevel.Error;
                break;
        }

        var errorDetails = new ErrorDetailsBaseReservation()
        {
            Type = exception.Error.GetType().Name,
            StatusCode = (int)httpStatusCode,
            Message = ErrorHandling.GetMessageException(exception.Error),
            Detail = exception.Error.StackTrace,
            LogLevel = logLevel
        };

        return errorDetails;
    }
}