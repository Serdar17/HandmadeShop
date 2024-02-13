using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Pages.Auth.Models;
using HandmadeShop.Web.Pages.Profile.Models;

namespace HandmadeShop.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> LoginAsync(LoginModel model);
    Task<Result> RegisterAsync(RegisterModel model);
    Task<Result> VerifyEmailAsync(VerifyEmailModel model);
    Task<Result> ForgotPasswordAsync(ForgotPasswordModel model);
    Task<Result> ResetPasswordAsync(ResetPasswordModel model);
    Task<Result> ResetProfilePasswordAsync(ResetProfilePasswordModel model);
    Task Logout();
}