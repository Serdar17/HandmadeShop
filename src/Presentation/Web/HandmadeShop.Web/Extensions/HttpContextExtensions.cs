using System.Text.Json;
using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Common;

namespace HandmadeShop.Web.Extensions;

public static class HttpContextExtensions
{
    public static async Task<Error> ToErrorAsync(this HttpContent httpContent)
    {
        var error = await httpContent.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ErrorResult>(error, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ErrorResult();

        return result.Errors.First();
    }
}