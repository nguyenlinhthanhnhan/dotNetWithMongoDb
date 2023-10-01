using System.Diagnostics;

namespace MongoWithDotnet.View.CRM.Middleware;

/// <summary>
/// This class is used to log the performance of the request
/// </summary>
public class PerformanceMiddleware
{
    private readonly ILogger<PerformanceMiddleware> _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// This method is used to log the performance of the request
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        const int performanceTimeLog = 500;

        var sw = new Stopwatch();

        sw.Start();

        await _next(context);

        sw.Stop();

        if (performanceTimeLog < sw.ElapsedMilliseconds)
            _logger.LogWarning("Request {Method} {Path} it took about {Elapsed} ms", context.Request?.Method,
                context.Request?.Path.Value, sw.ElapsedMilliseconds);
    }
}