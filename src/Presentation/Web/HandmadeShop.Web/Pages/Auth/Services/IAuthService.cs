using HandmadeShop.Web.Common;
using HandmadeShop.Web.Pages.Auth.Models;

namespace HandmadeShop.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> LoginAsync(LoginModel model);
    Task<Result> RegisterAsync(RegisterModel model);

    Task<Result> VerifyEmailAsync(VerifyEmailModel model);
    Task Logout();
}