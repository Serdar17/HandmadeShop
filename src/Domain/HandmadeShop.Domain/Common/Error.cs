using System.Text.Json.Serialization;
using HandmadeShop.Domain.Enums;

namespace HandmadeShop.Domain.Common;

public sealed record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    
    [JsonPropertyName("Code")]
    public string Code { get; }
    
    [JsonPropertyName("message")]
    public string Message { get; }
    
    [JsonPropertyName("errorType")]
    public ErrorType ErrorType { get; }

    [JsonConstructor]
    private Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        ErrorType = errorType;
        Message = message;
    }

    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);
    
    public static Error Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);
    
    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);

    public static Error Forbidden(string code, string message) =>
        new(code, message, ErrorType.Forbidden);
}