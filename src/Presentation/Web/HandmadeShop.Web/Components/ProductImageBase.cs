using HandmadeShop.Web.Pages.Product.Services;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Components;

public class ProductImageBase : ComponentBase
{
    [Inject] protected IProductService ProductService { get; set; }
    [Parameter] public Guid ProductId { get; set; }


    protected override async Task OnInitializedAsync()
    {
        // TODO: fetch product images by id
    }
}