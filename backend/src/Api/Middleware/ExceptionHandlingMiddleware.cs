using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = "application/json";

            var payload = new
            {
                error = "Duplicate resource",
                message = "A resource with the same unique value already exists."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException?.Message.Contains("duplicate key") == true
            || ex.InnerException?.Message.Contains("unique constraint") == true;
    }
}
