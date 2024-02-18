using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Extensions;
using HandmadeShop.Web.Pages.Product.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
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

    protected string SortOrder = "desc";
    protected string? SortColumn = string.Empty;
    protected int PriceFrom;
    protected int PriceTo;
    
    protected bool IsLoading { get; set; }
    
    protected PagedList<ProductModel>? PagedList;

    protected SortItem SortItem;
    
    protected List<SortItem> SortItems = new()
    {
        new SortItem("Последние", "date,desc"),
        new SortItem("Популярные", "popular,desc"),
        new SortItem("Сначала дорогие", "price,desc"),
        new SortItem("Сначала дешевле", "price,asc"),
    };

    protected override void OnInitialized()
    {
        SortItem = SortItems[0];
        base.OnInitialized();
    }

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
            SortColumn = sortColumn;
        }
    
        if (query.TryGetValue("sortOrder", out var sortOrder))
        {
            QueryModel.SortOrder = sortOrder;
            SortOrder = sortOrder;
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
            if (PriceFrom == 0)
            {
                PriceFrom = PagedList.MinPrice;
                PriceTo = PagedList.MaxPrice;
            }

            return;
        }
    }
    
    protected async Task Selected(int value)
    {
        Page = value;
        IsLoading = true;
        var query = GetQueryUrl("/catalog");
        QueryModel = new ProductQueryModel();
        QueryModel = new ProductQueryModel()
        {
            Page = Page,
            PageSize = PageCount,
            SortColumn = SortColumn,
            SortOrder = SortOrder
        };
        QueryModel.CatalogName = CatalogName;
        NavigationManager.NavigateTo($"/catalog/{CatalogName}{query}");
        await ReloadData(QueryModel);
        ShouldRender();
    }

    protected async Task SelectedSort(SortItem sortValue)
    {
        var values = sortValue.Value.Split(',');
        SortColumn = values[0];
        SortOrder = values[1];
        SortItem = sortValue;
        QueryModel = new ProductQueryModel()
        {
            Page = Page,
            PageSize = PageCount,
            SortColumn = SortColumn,
            SortOrder = SortOrder
        };
        QueryModel.CatalogName = CatalogName;
        var query = GetQueryUrl("/catalog");
        NavigationManager.NavigateTo($"/catalog/{CatalogName}{query}");
        await ReloadData(QueryModel);
        ShouldRender();
    }
    
    protected void OnRangeChanged(Price price)
    {
        Console.WriteLine("On range change");
        Console.WriteLine(price.From);
        PriceFrom = price.From;
        PriceTo = price.To;
    }
    
    private string GetQueryUrl(string url)
    {
        var queryUrl = new Uri(url)
            .AddParameter("page", Page.ToString());
    
        if (!string.IsNullOrEmpty(SortColumn))
            queryUrl = queryUrl.AddParameter("sortColumn", SortColumn);
    
        if (!string.IsNullOrEmpty(SortOrder))
            queryUrl = queryUrl.AddParameter("sortOrder", SortOrder);
    
        if (PriceFrom != null)
            queryUrl = queryUrl.AddParameter("priceFrom", PriceFrom.ToString());
        
        if (PriceTo != null)
            queryUrl = queryUrl.AddParameter("priceFrom", PriceTo.ToString());
    
        return queryUrl.Query;
    }
    

    
}

public class Price
{
    public int From { get; set; }
    public int To { get; set; }
}

public class SortItem
{
    public string Name { get; }

    public string Value { get; }

    public SortItem(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        var other = obj as SortItem;
        return other?.Name == Name;
    }

    public override string ToString() => Name;

    public override int GetHashCode() => Name.GetHashCode();
}