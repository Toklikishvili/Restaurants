using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context , RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        await next(context);

        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds > 4000)
        {
            var requestDetails = $"Method: {context.Request.Method}, Path: {context.Request.Path}, Elapsed Time: {stopwatch.ElapsedMilliseconds} ms";
            logger.LogInformation(requestDetails);
        }
    }
}
