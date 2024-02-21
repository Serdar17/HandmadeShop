using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.SharedModel.Reviews.Models;
using HandmadeShop.Web.Pages.Review.Services;
using Microsoft.AspNetCore.Components;
using HandmadeShop.Web.Pages.Product;
using HandmadeShop.Web.Pages.Profile;
using HandmadeShop.Web.Services;
using MudBlazor;

namespace HandmadeShop.Web.Components;

public class ProductCardBase : ComponentBase
{
    [Inject] private IReviewService ReviewService { get; set; }
    [Inject] private IClipboardService ClipboardService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    
    [Parameter]
    public bool IsEditable { get; set; }

    [Parameter] public ProductModel Model { get; set; }

    [CascadingParameter] public List<Guid> FavoriteProducts { get; set; } 
    [CascadingParameter] public Catalog? Catalog { get; set; }
    
    [CascadingParameter] public MyFavorites? Favorites { get; set; }
 
    [Inject] protected NavigationManager NavigationManager { get; set; }

    protected bool ToggleValue => FavoriteProducts.Contains(Model.Uid);
    
    protected void Click()
    {
        NavigationManager.NavigateTo($"product/info/{Model.Uid}");
    }

    protected void CopyToClipboard()
    {
        ClipboardService.CopyToClipboard($"{Settings.WebRoot}/product/info/{Model.Uid}");
        Snackbar.Add("Ссылка скопирована!", Severity.Info);
    }

    protected void OnToggleChanged(bool toggled)
    {
        if (toggled)
        {
            FavoriteProducts.Add(Model.Uid);
            ReviewService.AddFavoriteAsync(new AddFavoriteModel { ProductId = Model.Uid });
        }
        else
        {
            FavoriteProducts.Remove(Model.Uid);
            ReviewService.RemoveFavoriteAsync(new RemoveFavoriteModel() { ProductId = Model.Uid });
        }
        
        Catalog?.NotifyChanged();
        Favorites?.NotifyChanged(Model.Uid);
    }
}