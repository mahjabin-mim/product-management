using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ProductValidation.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;
        private static readonly JsonSerializerOptions jsonOptions = new() 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
            WriteIndented = true // pretty formated json
        };

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred while processing the request.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                // Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1", // Optional: Link to documentation about the error
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred. Please try again later.",
                Instance = context.Request.Path
            };
            // Add additional details to the problem details if needed
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            problemDetails.Extensions["timestamp"] = DateTime.UtcNow;

            var json = JsonSerializer.Serialize(problemDetails, jsonOptions);

            await context.Response.WriteAsync(json);
        }
    }
}

