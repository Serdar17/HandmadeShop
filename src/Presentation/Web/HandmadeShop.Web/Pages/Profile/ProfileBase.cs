using HandmadeShop.Web.Components;
using HandmadeShop.Web.Extensions;
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
    protected bool Processing { get; set; }

    private StreamContent stream;

    protected string DataUrl = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(4000);
        var result = await AccountService.GetAccountInfoAsync();
        
        if (result.IsFailure)
            await AuthService.Logout();

        Model = result.Value;
    }

    protected IBrowserFile? Avatar;
    protected async Task UploadFiles(InputFileChangeEventArgs args)
    {
        Avatar = args.File;
        DataUrl = await Avatar.GetDataUrl();
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
        Processing = true;

        var imageBytes = Convert.FromBase64String(DataUrl.Split(',')[1]);
        using MemoryStream ms = new MemoryStream(imageBytes);
        using var stream1 = new StreamContent(ms);
        var form = new MultipartFormDataContent();
        form.Add(stream1,"avatar", Avatar.Name);
        var result = await AccountService.UploadAvatarAsync(form);
        
        if (result.IsSuccess)
        {
            Model = result.Value;
            Snackbar.Add("Аватар загружен успешно!", Severity.Success);
            Avatar = null;
        }

        Processing = false;
    }

    protected void RemoveFile()
    {
        Avatar = null;
    }
}