using HandmadeShop.Web.Pages.Auth.Models;
using HandmadeShop.Web.Pages.Auth.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Auth;

public class RegisterBase : ComponentBase
{
    [Inject] 
    private  IAuthService AuthService { get; set; }
    [Inject] 
    private NavigationManager NavigationManager { get; set; }
 
    protected bool Success;
    protected MudForm Form;

    protected RegisterModel Model = new()
    {
        Name = "Serdar",
        Email = "useinovserdar23@gmail.com",
        Password = "12345"
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
        Error = "";
        ErrorDetail = "";

        var result = await AuthService.RegisterAsync(Model);

        if (result.IsSuccess)
        {
            Success = true;
            // NavigationManager.NavigateTo("/");
        }
        else
        {
            Error = result.Error.Code;
            ErrorDetail = result.Error.Message;
            ShowErrors = true;
        }
    }
}