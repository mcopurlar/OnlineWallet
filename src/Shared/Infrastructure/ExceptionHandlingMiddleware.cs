using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineWallet.Infrastructure.Validations;

namespace OnlineWallet.Infrastructure;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occured : {Message}", e.Message);

            var problemDetails = new ProblemDetails
            {
                Title = "Unhandled error.",
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231/#section-6.6.1"
            };
            var responseStatusCode = StatusCodes.Status500InternalServerError;
            var problemDetailsAsJson = JsonSerializer.Serialize(problemDetails);


            if (e is BusinessValidationException || e is InvalidRequestException)
            {
                var exceptionInvalidResults = (e as BaseException).InvalidResults;

                var listOfProblemDetails = exceptionInvalidResults.Select(GetBadRequestProblemDetails);

                responseStatusCode = StatusCodes.Status400BadRequest;
                problemDetailsAsJson = JsonSerializer.Serialize(listOfProblemDetails);
            }

            context.Response.StatusCode = responseStatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(problemDetailsAsJson, Encoding.UTF8);
        }
    }

    private static ProblemDetails GetBadRequestProblemDetails(InvalidResult validationResult)
    {
        return new ProblemDetails
        {
            Title = validationResult.ErrorMessage.Description,
            Detail = validationResult.ErrorMessage.Code,
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231/#section-6.5.1"
        };
    }
}