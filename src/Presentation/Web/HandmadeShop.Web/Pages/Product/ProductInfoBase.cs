using HandmadeShop.SharedModel.Basket.Models;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Layout;
using HandmadeShop.Web.Pages.Basket.Services;
using HandmadeShop.Web.Pages.Product.Services;
using HandmadeShop.Web.TransferServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Product;

public class ProductInfoBase : ComponentBase
{
    [Parameter] public Guid ProductId { get; set; }
    
    [CascadingParameter] public BasketModel BasketModel { get; set; }
    [CascadingParameter] public MainLayout MainLayout { get; set; }
    
    [Inject] protected IProductService ProductService { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected IBasketService BasketService { get; set; }
    [Inject] protected BasketTransferService BasketTransferService { get; set; }
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

    protected async Task AddToBasket()
    {
        var model = new AddCartModel
        {
            ProductId = Model.Uid,
        };

        var result = await BasketService.AddCartAsync(model);

        if (result.IsSuccess && result.Value != null)
        {
            BasketTransferService.Data = result.Value;
        }

    }

}