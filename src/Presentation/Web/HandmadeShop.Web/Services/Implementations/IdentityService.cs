using Microsoft.AspNetCore.Components.Authorization;

namespace HandmadeShop.Web.Services;

public class IdentityService(AuthenticationStateProvider provider) : IIdentityService
{
    private AuthenticationStateProvider _provider = provider ?? throw new ArgumentNullException(nameof(provider));

    public async Task<string?> GetClaimsPrincipalData()
    {
        var authState = await _provider.GetAuthenticationStateAsync();
        
        var user = authState.User;
        
        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            return user.FindFirst("sub")?.Value;    
        }
        
        return string.Empty;
    }
}