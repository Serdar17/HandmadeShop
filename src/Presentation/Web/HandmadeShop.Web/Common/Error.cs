using System.Text.Json.Serialization;

namespace HandmadeShop.Web.Common;

public sealed record Error
{
    public static readonly Error None = new(string.Empty, string.Empty);
    
    [JsonPropertyName("Code")]
    public string Code { get;}
    
    [JsonPropertyName("message")]
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}