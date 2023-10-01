using MongoWithDotnet.Application.Exceptions;
using Newtonsoft.Json;
using MongoWithDotnet.Core.Exceptions;
using MongoWithDotnet.Shared.DTO;

namespace MongoWithDotnet.View.CRM.Middleware;

/// <summary>
/// Middleware for handling exceptions
/// </summary>
public class HandlingExceptionMiddleware
{
    private readonly ILogger<HandlingExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public HandlingExceptionMiddleware(RequestDelegate next, ILogger<HandlingExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// This method is used to handle exceptions
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
        _logger.LogError("Exception: {Message}", ex.Message);

        var code = StatusCodes.Status500InternalServerError;
        var errors = new List<string> { ex.Message };

        code = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ResourceNotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            UnprocessableRequestException => StatusCodes.Status422UnprocessableEntity,
            _ => code
        };

        var result = JsonConvert.SerializeObject(ApiResult<string>.Failure(errors));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(result);
    }
}