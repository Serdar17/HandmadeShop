using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Pages.Product.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.WebUtilities;

namespace HandmadeShop.Web.Pages.Product;

public class CatalogBase : ComponentBase
{
    [Parameter] public string CatalogName { get; set; }
    
    [Inject] protected IProductService ProductService { get; set; }
    
    [Inject] protected NavigationManager NavigationManager { get; set; }

    protected ProductQueryModel QueryModel;

    protected int PageCount => (int)Math.Ceiling((double)PagedList.TotalCount / PageSize);
    
    protected int Page { get; set; } = 1;
    protected int PageSize { get; set; } = 4;
    
    protected bool IsLoading { get; set; }
    
    protected PagedList<ProductModel>? PagedList;

    protected override async Task OnParametersSetAsync()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);
        var QueryModel = new ProductQueryModel
        {
            PageSize = PageSize,
            CatalogName = CatalogName
        };
        
        if (query.TryGetValue("page", out var page))
        {
            QueryModel.Page = int.Parse(page);
            Page = int.Parse(page);
        }
        else
        {
            QueryModel.Page = Page;
        }
    
        if (query.TryGetValue("sortColumn", out var sortColumn))
        {
            QueryModel.SortColumn = sortColumn;
        }
    
        if (query.TryGetValue("sortOrder", out var sortOrder))
        {
            QueryModel.SortOrder = sortOrder;
        }
        await ReloadData(QueryModel);
    }

    private async Task ReloadData(ProductQueryModel queryModel)
    {
        IsLoading = true;
        Console.WriteLine("reload data");
        var result = await ProductService.GetProductsByQueryAsync(queryModel);
            
        if (result.IsSuccess && result.Value != null)
        {
            PagedList = result.Value;
            StateHasChanged();
            IsLoading = false;
            return;
        }
    }
    
    protected async Task Selected(int value)
    {
        Page = value;
        IsLoading = true;
        NavigationManager.NavigateTo($"/catalog/{CatalogName}?page={Page}", true);
    }
}