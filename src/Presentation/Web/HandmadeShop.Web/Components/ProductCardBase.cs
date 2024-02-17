using HandmadeShop.SharedModel.Catalogs.Models;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Components;

public class ProductCardBase : ComponentBase
{
    [Parameter]
    public bool IsEditable { get; set; }

    [Parameter] public ProductModel Model { get; set; }
 
    [Inject] protected NavigationManager NavigationManager { get; set; }
    
    protected void Click()
    {
        NavigationManager.NavigateTo($"product/info/{Model.Uid}");
    }

    protected void CopyToClipboard()
    {
       // TODO доделать 
    }
}