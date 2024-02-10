using Duende.IdentityServer.Models;
using HandmadeShop.Common.Security;

namespace HandmadeShop.Identity.Configuration.IS4;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =
        new List<ApiScope>
        {
            new ApiScope(AppScopes.BooksRead, "Access to books API - Read data"),
            new ApiScope(AppScopes.BooksWrite, "Access to books API - Write data")
        };
}