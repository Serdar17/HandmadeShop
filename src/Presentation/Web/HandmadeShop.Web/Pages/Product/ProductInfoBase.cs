using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Pages.Product.Models;
using HandmadeShop.Web.Pages.Product.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Product;

public class ProductInfoBase : ComponentBase
{
    [Parameter] public Guid ProductId { get; set; }
    
    [Inject] protected IProductService ProductService { get; set; }
    
    [Inject] protected ISnackbar Snackbar { get; set; }
    
    protected bool IsLoading { get; set; }

    protected ProductInfoModel? Model;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        await Task.Delay(2000);
        var result = await ProductService.GetProductInfoModel(ProductId);
        
        if (result.IsSuccess && result.Value != null)
        {
            Model = result.Value;
            IsLoading = false;
            return;
        }

        Snackbar.Add("Произошла ошибка загрузки", Severity.Error);
        IsLoading = false;
    }

}