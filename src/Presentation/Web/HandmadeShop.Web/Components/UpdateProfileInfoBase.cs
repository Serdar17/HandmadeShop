using HandmadeShop.Domain.Enums;
using HandmadeShop.Web.Pages.Profile.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Components;

public class UpdateProfileInfoBase : ComponentBase
{
    [Parameter] public AccountInfoModel Model { get; set; }
    
    [Inject] protected 
    
    protected MudForm Form;

    public Gender Gender { get; set; } = Gender.Unknown;
    
    protected bool ShowErrors;
    protected string ErrorDetail = string.Empty;
    
    protected bool IsSuccess { get; set; }
    protected string[] ValidationErrors;

    protected async Task OnValidSubmit()
    {
        
    }
}