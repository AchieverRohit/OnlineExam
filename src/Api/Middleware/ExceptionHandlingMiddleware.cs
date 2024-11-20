namespace thinkschool.OnlineExam.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next; // The next middleware in the request pipeline.
    private readonly ILogger<ExceptionHandlingMiddleware> _logger; // Logger instance to log errors.

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next; // Assigning the next middleware in the pipeline.
        _logger = logger; // Assigning the logger instance.
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Invoke the next middleware in the pipeline.
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex); // Handle exceptions thrown by the next middleware.
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, ConstantsValues.UnhandledException); // Log the exception.

        // Determine the status code based on the exception type.
        var code = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ResourceNotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            UnprocessableRequestException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

        var result = JsonConvert.SerializeObject(ex.Message); // Serialize the exception message to JSON.
        context.Response.ContentType = ConstantsValues.ApplicationJson; // Set the response content type.
        context.Response.StatusCode = code; // Set the response status code.
        await context.Response.WriteAsync(result); // Write the exception message to the response.
    }
}

