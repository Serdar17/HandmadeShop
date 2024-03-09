using System.Text.Json;
using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Common;

namespace HandmadeShop.Web.Extensions;

public static class StringExtensions
{
    public static Error ToError(this string content)
    {
        var result = JsonSerializer.Deserialize<ErrorResult>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ErrorResult();

        return result.Errors.First();
    }
}