using HandmadeShop.Web.Common;
using HandmadeShop.Web.Components;
using HandmadeShop.Web.Pages.Auth.Services;
using HandmadeShop.Web.Pages.Profile.Models;
using HandmadeShop.Web.Pages.Profile.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Profile;

public class ProfileBase : ComponentBase
{
    [Inject]
    private IAccountService AccountService { get; set; }
    
    [Inject]
    private IAuthService AuthService { get; set; }
    
    [Inject]
    private ISnackbar Snackbar { get; set; }
    
    [Inject] private IDialogService DialogService { get; set; }
    
    protected AccountInfoModel? Model;
    protected ResetProfilePasswordModel ResetPwdModel = new();
    protected bool IsSuccess { get; set; }
    
    protected bool PasswordVisibility;
    protected bool PasswordConfirmVisibility;
    protected InputType PasswordInput = InputType.Password;
    protected InputType PasswordConfirmInput = InputType.Password;
    protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    protected string PasswordConfirmInputIcon = Icons.Material.Filled.VisibilityOff;

    private StreamContent stream;

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

    protected IBrowserFile? Avatar;
    protected async Task UploadFiles(InputFileChangeEventArgs args)
    {
        Avatar = args.File;
        stream = new StreamContent(args.File.OpenReadStream());
    }

    protected async Task DeleteAsync()
    {
        var parameters = new DialogParameters<Dialog>
        {
            { x => x.ContentText, "Вы действительно хотите удалить Аватар?" },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>("Удаление", parameters, options);
        
        if ((await dialog.Result).Canceled)
            return;
        
        var result = await AccountService.DeleteAvatarAsync();

        if (result.IsSuccess && result.Value is not null)
        {
            Model = result.Value;
            return;
        }
    }

    protected async Task UploadAsync()
    {
        var form = new MultipartFormDataContent();
        form.Add(stream, "avatar", Avatar.Name);
        var result = await AccountService.UploadAvatarAsync(form);

        if (result.IsSuccess)
        {
            Model = result.Value;
            Snackbar.Add("Аватар загружен успешно!", Severity.Success);
            Avatar = null;
            return;
        }
    }
}