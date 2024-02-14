using HandmadeShop.Web.Pages.Auth.Models;
using HandmadeShop.Web.Pages.Auth.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Auth;

public class ResetPasswordBase : ComponentBase
{
    [Inject]
    private IAuthService _authService { get; set; }

    [Inject] 
    private NavigationManager _navigationManager { get; set; }

    protected MudForm Form;
    
    protected bool ShowErrors;
    protected string ErrorDetail = string.Empty;
    
    protected bool IsSuccess { get; set; }
    protected string[] ValidationErrors;
    
    protected string Email { get; set; }
    protected string Token { get; set; }

    protected ResetPasswordModel Model = new();
    
    protected override async Task OnInitializedAsync()
    {
        var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);
        
        if(query.TryGetValue("email", out var email))
        {
            Email = email;
        }

        if (query.TryGetValue("token", out var token))
        {
            Token = token;
        }

        Model.Email = email;
        Model.Token = token;
    }

    protected async Task OnValidSubmit()
    {
        await Form.Validate();

        if (!Form.IsValid)
            return;

        var result = await _authService.ResetPasswordAsync(Model);
        
        if (result.IsSuccess)
            _navigationManager.NavigateTo("/login");
        ShowErrors = true;
        ErrorDetail = result.Error.Message;

    }

    protected string PasswordMatch(string arg)
    {
        if (Model.Password != arg)
            return "Passwords don't match";
        
        return null;
    }
}