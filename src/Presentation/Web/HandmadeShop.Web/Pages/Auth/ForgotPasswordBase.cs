using HandmadeShop.Web.Pages.Auth.Models;
using HandmadeShop.Web.Pages.Auth.Services;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Pages.Auth;

public class ForgotPasswordBase : ComponentBase
{
    [Inject] 
    private IAuthService _authService { get; set; }
    
    protected ForgotPasswordModel Model = new();

    protected string Error = string.Empty;
    protected string ErrorDetail = string.Empty;
    protected bool ShowErrors;
    protected bool IsSuccess;
    

    protected async Task OnValidSubmit()
    {
        ShowErrors = false;

        var result = await _authService.ForgotPasswordAsync(Model);

        if (result.IsSuccess)
        {
            IsSuccess = true;
        }
        else
        {
            Error = result.Error.Code;
            ErrorDetail = result.Error.Message;
            ShowErrors = true;
        }
    }
}