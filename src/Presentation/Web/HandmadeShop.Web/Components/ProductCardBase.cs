using HandmadeShop.Domain;
using HandmadeShop.Web.Pages.Product.Models;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Components;

public class ProductCardBase : ComponentBase
{
    [Parameter]
    public bool IsEditable { get; set; }

    [Parameter] public ProductModel Model { get; set; }
    
    protected void Click()
    {
        Console.WriteLine("Click");
    }
}