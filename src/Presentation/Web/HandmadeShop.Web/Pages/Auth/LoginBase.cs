using HandmadeShop.Web.Pages.Auth.Models;
using HandmadeShop.Web.Pages.Auth.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Auth;

public class LoginBase : ComponentBase
{
    [Inject] 
    private  IAuthService AuthService { get; set; }
    [Inject] 
    private NavigationManager NavigationManager { get; set; }
 
    protected bool Success;
    protected MudForm Form;

    protected LoginModel Model = new()
    {
        Email = "useinovserdar23@gmail.com",
        Password = "1234"
    };

    protected bool PasswordVisibility;
    protected InputType PasswordInput = InputType.Password;
    protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    protected bool ShowErrors;
    protected string Error = string.Empty;
    protected string ErrorDetail = string.Empty;

    protected void TogglePasswordVisibility()
    {
        if (PasswordVisibility)
        {
            PasswordVisibility = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            PasswordVisibility = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }

    protected async Task OnValidSubmit()
    {
        ShowErrors = false;

        var result = await AuthService.LoginAsync(Model);

        if (result.Successful)
        {
            NavigationManager.NavigateTo("/");
        }
        else
        {
            Error = result.Error;
            ErrorDetail = result.ErrorDescription;
            ShowErrors = true;
        }
    }
}