using HandmadeShop.Web.Pages.Auth.Models;

namespace HandmadeShop.Web.Services;

public interface ITokenService
{
    Task<LoginResult> RefreshTokenAsync();
}