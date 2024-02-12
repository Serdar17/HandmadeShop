using HandmadeShop.Web.Pages.Auth.Models;

namespace HandmadeShop.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}