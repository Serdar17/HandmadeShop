using System.Text.Json;
using HandmadeShop.Common.Exceptions;
using HandmadeShop.Common.Extensions;
using HandmadeShop.Common.Responses;

namespace HandmadeShop.Api.Middlewares;

/// <summary>
/// Exception handling middleware
/// </summary>
public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor for Exception handling middleware
    /// </summary>
    /// <param name="next">RequestDelegate</param>
    public ExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse? response = null;
        try
        {
            await _next.Invoke(context);
        }
        catch (ProcessException pe)
        {
            response = pe.ToErrorResponse();
        }
        catch (Exception pe)
        {
            response = pe.ToErrorResponse();
        }
        finally
        {
            if (response is not null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
            }
        }
    }
}