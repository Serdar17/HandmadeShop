using HandmadeShop.Web.Common;
using HandmadeShop.Web.Pages.Auth.Services;
using HandmadeShop.Web.Pages.Profile.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Components;

public class ChangePasswordBase : ComponentBase
{
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IAuthService AuthService { get; set; }
                        
    protected bool PasswordVisibility;
    protected bool PasswordConfirmVisibility;
    protected InputType PasswordInput = InputType.Password;
    protected InputType PasswordConfirmInput = InputType.Password;
    protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    protected string PasswordConfirmInputIcon = Icons.Material.Filled.VisibilityOff;
    protected string ErrorDetail = string.Empty;
    protected bool ShowErrors;
    protected ResetProfilePasswordModel ResetPwdModel = new();
    
    protected bool IsSuccess { get; set; }
    
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
            Snackbar.Add("Пароль успешно изменен!", Severity.Success);
            ResetPwdModel.Password = string.Empty;
            ResetPwdModel.ConfirmPassword = string.Empty;
            return;
        }
        
        ShowErrors = true;
        ErrorDetail = result.Error.Message;
    }
    
    protected string PasswordMatch(string arg)
    {
        if (ResetPwdModel.Password != arg)
        {
            IsSuccess = false;
            return "Passwords don't match";
        }

        IsSuccess = true;
        return null;
    }
}