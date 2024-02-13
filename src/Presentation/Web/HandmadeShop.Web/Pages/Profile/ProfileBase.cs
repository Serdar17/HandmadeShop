using HandmadeShop.Web.Common;
using HandmadeShop.Web.Pages.Auth.Services;
using HandmadeShop.Web.Pages.Profile.Models;
using HandmadeShop.Web.Pages.Profile.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Profile;

public class ProfileBase : ComponentBase
{
    [Inject]
    private IAccountService AccountService { get; set; }
    
    [Inject]
    private IAuthService AuthService { get; set; }
    
    protected AccountInfoModel Model;
    protected ResetProfilePasswordModel ResetPwdModel = new();
    protected bool IsSuccess { get; set; }
    
    protected bool PasswordVisibility;
    protected bool PasswordConfirmVisibility;
    protected InputType PasswordInput = InputType.Password;
    protected InputType PasswordConfirmInput = InputType.Password;
    protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    protected string PasswordConfirmInputIcon = Icons.Material.Filled.VisibilityOff;

    protected string ErrorDetail = string.Empty;
    protected bool ShowErrors;

    protected override async Task OnInitializedAsync()
    {
        var result = await AccountService.GetAccountInfoAsync();
        
        if (result.IsFailure)
            await AuthService.Logout();

        Model = result.Value;
    }

    protected void ChangeVisibility(PasswordType type)
    {
        if (type == PasswordType.Password)
        {
            TogglePasswordVisibility(ref PasswordVisibility, ref PasswordInput, ref PasswordInputIcon);
        }
        else
        {
            TogglePasswordVisibility(
                ref PasswordConfirmVisibility, 
                ref PasswordConfirmInput, 
                ref PasswordConfirmInputIcon);
        }
    }
    
    // protected void ToggleConfirmPasswordVisibility()
    // {
    //     if (PasswordConfirmVisibility)
    //     {
    //         PasswordConfirmVisibility = false;
    //         PasswordConfirmInputIcon = Icons.Material.Filled.VisibilityOff;
    //         PasswordConfirmInput = InputType.Password;
    //     }
    //     else
    //     {
    //         PasswordConfirmVisibility = true;
    //         PasswordConfirmInputIcon = Icons.Material.Filled.Visibility;
    //         PasswordConfirmInput = InputType.Text;
    //     }
    // }
    
    protected void TogglePasswordVisibility(ref bool visibility, ref InputType input, ref string icon)
    {
        if (visibility)
        {
            visibility = false;
            icon = Icons.Material.Filled.VisibilityOff;
            input = InputType.Password;
        }
        else
        {
            visibility = true;
            icon = Icons.Material.Filled.Visibility;
            input = InputType.Text;
        }
    }

    protected async Task OnValidSubmit()
    {
        if (ResetPwdModel.Password != ResetPwdModel.ConfirmPassword)
        {
            ShowErrors = true;
            ErrorDetail = "Пароли должны совпадать!!!";
            return;
        }
        
        var result = await AuthService.ResetProfilePasswordAsync(ResetPwdModel);

        if (result.IsSuccess)
        {
            return;
        }
        
        ShowErrors = true;
        ErrorDetail = result.Error.Message;
    }
    
    protected string PasswordMatch(string arg)
    {
        if (ResetPwdModel.Password != arg)
            return "Passwords don't match";
        
        return null;
    } 
}