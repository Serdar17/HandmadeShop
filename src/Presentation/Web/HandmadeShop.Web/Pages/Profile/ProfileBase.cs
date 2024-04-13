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
    
    protected AccountInfoModel? Model;
    private StreamContent stream;

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(500);
        var result = await AccountService.GetAccountInfoAsync();
        
        if (result.IsFailure)
            await AuthService.Logout();

        Model = result.Value;
    }
}