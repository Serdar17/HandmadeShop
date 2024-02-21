using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Pages.Profile.Services;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Pages.Profile;

public class MyFavoritesBase : ComponentBase
{
    [Inject] private IAccountService AccountService { get; set; }

    protected int Page { get; set; } = 1;
    protected int PageSize { get; set; } = 3;

    protected PagedList<ProductModel>? Products;

    protected List<Guid> FavoriteProducts;
    
    protected bool IsLoading;

    protected override async Task OnInitializedAsync()
    {
        var result = await AccountService.GetAllFavoriteAsync();
        
        if (result.IsSuccess && result.Value != null)
        {
            FavoriteProducts = result.Value.ToList();
        }
        await ReloadData(false);
    }

    public async Task LoadProducts()
    {
        Console.WriteLine("called load");
        Page += 1;
        await ReloadData(true);
    }
    
    private async Task ReloadData(bool isAdd)
    {
        var query = new ProductQueryModel
        {
            Page = Page,
            PageSize = PageSize,
            IsFavorite = true,
        };

        var result = await AccountService.GetUserProducts(query);
        
        if (result.IsSuccess && result.Value != null)
        {
            if (isAdd)
            {
                Products.Items.AddRange(result.Value.Items);
            }
            else
            {
                Products = result.Value; 
            }
            
        }
    }
    
    public void NotifyChanged(Guid id)
    {
        var product = Products.Items.First(x => x.Uid == id);
        Products.Items.Remove(product);
        InvokeAsync(StateHasChanged);
    }
}