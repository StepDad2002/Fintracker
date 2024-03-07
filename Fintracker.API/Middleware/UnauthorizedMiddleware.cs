﻿using Fintracker.Application.Responses;

namespace Fintracker.API.Middleware;

public class UnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            var response = new UnauthorizedResponse
            {
                Id = Guid.NewGuid(),
                When = DateTime.UtcNow,
                Reason = "Unauthorized",
                Details = "The user is not authorized to access this resource",
                StatusCode = StatusCodes.Status401Unauthorized,
            };
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

public static class UnauthorizedMiddlewareExtensions
{
    public static IApplicationBuilder UseUnauthorizedMiddleware(this IApplicationBuilder app)
    {
        
        return app.UseMiddleware<UnauthorizedMiddleware>();
    }
}