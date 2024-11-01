using Gnarly.Data;
using NuGet.Next.Core.Exceptions;
using NuGet.Next.Protocol.Models;

namespace NuGet.Next.Middlewares;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware, ISingletonDependency
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            if (context.Request.Path == "/")
            {
                context.Request.Path = "/index.html";
            }

            await next(context);


            if (context.Response.StatusCode == 404)
            {
                context.Request.Path = "/index.html";

                await next(context);
            }
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";

            var response = new OkResponse(false, notFoundException.Message);

            await context.Response.WriteAsJsonAsync(response);

            logger.LogWarning("Resource not found: {Path}", context.Request.Path);
        }
        catch (UnauthorizedAccessException)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var response = new OkResponse(false, "未授权的访问");

            await context.Response.WriteAsJsonAsync(response);

            logger.LogWarning("Unauthorized access to {Path}", context.Request.Path);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new OkResponse(false, "服务器内部错误");

            await context.Response.WriteAsJsonAsync(response);

            logger.LogError(ex, "An error occurred while processing {Path}", context.Request.Path);
        }
    }
}