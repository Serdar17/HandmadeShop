using HandmadeShop.SharedModel.Accounts.Models;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Pages.Profile.Services;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Components;

public class MyProductsBase : ComponentBase
{
    [Inject] protected IAccountService AccountService { get; set; }

    protected PagedList<ProductModel>? Products;
    
    protected int Page { get; set; } = 1;
    protected int PageSize { get; set; } = 4;
    
    protected override async Task OnInitializedAsync()
    {
        await ReloadData(false);
    }
    
    public async Task LoadProducts()
    {
        Console.WriteLine("called load");
        Page += 1;
        await ReloadData(true);
    }

    protected async Task ReloadData(bool isAdd)
    {
        var query = new ProductQueryModel
        {
            Page = Page,
            PageSize = PageSize,
            IsFavorite = false,
        };

        var result = await AccountService.GetUserProducts(query);

        if (!result.IsSuccess || result.Value is null)
        {
            return;
        }

        if (isAdd)
        {
            Products?.Items.AddRange(result.Value.Items);
        }
        else
        {
            Products = result.Value;
        }
        
    }
}