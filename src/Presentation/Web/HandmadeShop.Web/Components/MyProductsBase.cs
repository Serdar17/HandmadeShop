using HandmadeShop.Web.Pages.Profile.Models;
using HandmadeShop.Web.Pages.Profile.Services;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Components;

public class MyProductsBase : ComponentBase
{
    [Inject] protected IAccountService AccountService { get; set; }

    protected UserProductModel? Model;


    protected override async Task OnInitializedAsync()
    {
        var result = await AccountService.GetUserProducts();

        if (result.IsSuccess && result.Value is not null)
        {
            Model = result.Value;
        }
    }
}