namespace thinkschool.OnlineExam.Api.Middleware;
public class PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<PerformanceMiddleware> _logger = logger;
    public async Task Invoke(HttpContext context)
    {
        const int PerformanceTimeThreshold = 500; // Time in milliseconds
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds > PerformanceTimeThreshold)
        {
            _logger.LogWarning(ConstantsValues.RequestTimeLog,
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds);
        }
    }
}


