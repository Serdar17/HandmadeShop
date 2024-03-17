using HandmadeShop.Web.Pages.Profile.Models;
using HandmadeShop.Web.Pages.Profile.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Components;

public class UpdateProfileInfoBase : ComponentBase
{
    [Parameter] public AccountInfoModel Model { get; set; }
    
    [Inject] protected IAccountService AccountService { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    
    protected MudForm Form;

    protected bool ShowErrors;
    protected string ErrorDetail = string.Empty;
    
    protected bool IsSuccess { get; set; }
    protected string[] ValidationErrors;
    

    protected async Task OnValidSubmit()
    {
        ShowErrors = false;
        ErrorDetail = "";

        var result = await AccountService.UpdateAccountInfoModel(Model);

        if (result.IsSuccess && result.Value is not null)
        {
            Model = result.Value;
            Snackbar.Add("Данные сохранены успешно!", Severity.Success);
        }
        else
        {
            ErrorDetail = result.Error.Message;
            ShowErrors = true;
        }

        IsSuccess = false;
    }
}