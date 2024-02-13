using System.Text.Json.Serialization;
using HandmadeShop.Domain.Common;

namespace HandmadeShop.Web.Common;

public class ErrorResult
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }
    
    [JsonPropertyName("errors")] 
    public List<Error> Errors { get; set; }
}