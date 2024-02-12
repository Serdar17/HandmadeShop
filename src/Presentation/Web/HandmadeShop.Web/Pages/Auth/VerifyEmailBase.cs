using HandmadeShop.Web.Pages.Auth.Models;
using HandmadeShop.Web.Pages.Auth.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace HandmadeShop.Web.Pages.Auth;

public class VerifyEmailBase : ComponentBase
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject] 
    protected IAuthService AuthService { get; set; }

    protected Guid UserId { get; set; }
    protected string Token { get; set; }
    
    protected bool IsSuccess { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);
        
        if(query.TryGetValue("userId", out var userId))
        {
            UserId = Guid.Parse(userId);
        }

        if (query.TryGetValue("token", out var token))
        {
            Token = token;
        }

        var model = new VerifyEmailModel(UserId, Token);
        var result = await AuthService.VerifyEmailAsync(model);

        if (result.IsSuccess)
            IsSuccess = true; 
    }
}