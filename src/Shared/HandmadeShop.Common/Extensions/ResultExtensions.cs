using HandmadeShop.Domain.Common;
using HandmadeShop.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace HandmadeShop.Common.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.ErrorType),
            title: GetTitle(result.Error.ErrorType),
            extensions: new Dictionary<string, object?>
            {
                { "errors", new[] { result.Error } }
            });
    }

    private static int GetStatusCode(ErrorType errorType)
    {
        switch (errorType)
        {
            case ErrorType.Validation:
                return StatusCodes.Status400BadRequest;
            case ErrorType.NotFound:
                return StatusCodes.Status404NotFound;
            case ErrorType.Conflict:
                return StatusCodes.Status409Conflict;
            case ErrorType.Forbidden:
                return StatusCodes.Status403Forbidden;
            default:    
                return StatusCodes.Status500InternalServerError;
        }
    }

    private static string GetTitle(ErrorType errorType)
    {
        switch (errorType)
        {
            case ErrorType.Validation:
                return "Bad request";
            case ErrorType.NotFound:
                return "Not found";
            case ErrorType.Conflict:
                return "Conflict";
            default:    
                return "Server failure";
        }
    }
}