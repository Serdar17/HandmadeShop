using Duende.IdentityServer.Models;
using HandmadeShop.Common.Security;

namespace HandmadeShop.Identity.Configuration.IS4;

public static class AppClients
{
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "swagger",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AccessTokenLifetime = 3600 * 24, // 24 hours

                AllowedScopes = {
                    AppScopes.BooksRead,
                    AppScopes.BooksWrite
                }
            }
            ,
            new Client
            {
                ClientId = "frontend",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowOfflineAccess = true, // returns refresh-token
                AccessTokenType = AccessTokenType.Jwt,

                AccessTokenLifetime = 3600 * 24, // 24 hours 

                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = 2592000, // 30 days
                SlidingRefreshTokenLifetime = 1296000, // 15 days

                AllowedScopes = {
                    AppScopes.BooksRead,
                    AppScopes.BooksWrite
                }
            }
        };
}