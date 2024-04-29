using HandmadeShop.Web.Components;
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
    [Inject] private IDialogService DialogService { get; set; }
 
    protected bool Success;
    protected MudForm Form;

    protected RegisterModel Model = new()
    {
        Name = "Buyer",
        Email = "buyer@mail.com",
        Password = "1234"
    };

    protected bool PasswordVisibility;
    protected InputType PasswordInput = InputType.Password;
    protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    protected bool ShowErrors;
    protected string Error = string.Empty;
    protected string ErrorDetail = string.Empty;
    
    protected async Task DeleteProduct()
    {
        var parameters = new DialogParameters
        {
            {
                "ContentText",
                "Вы действительно хотите удалить товар? Данное действие нельзя будет отменить после удаления." +
                "Также при удалении пропадет рейтинг товара и все отзывы!"
            },
            { "ButtonText", "Delete" },
            { "Color", Color.Error }
        };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>("Удаление продукта", parameters, options);
        var result = await dialog.Result;

        if (result.Canceled)
        {
            return;
        }
    }
    
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