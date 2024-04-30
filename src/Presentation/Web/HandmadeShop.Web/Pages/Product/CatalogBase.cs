using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Extensions;
using HandmadeShop.Web.Pages.Product.Models;
using HandmadeShop.Web.Pages.Product.Services;
using HandmadeShop.Web.Pages.Profile.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;


namespace HandmadeShop.Web.Pages.Product;

public class CatalogBase : ComponentBase
{
    [Parameter] public string CatalogName { get; set; }
    
    [Inject] protected IProductService ProductService { get; set; }
    
    [Inject] protected IAccountService AccountService { get; set; } = null!;

    [Inject] protected NavigationManager NavigationManager { get; set; }

    private const string Path = "/catalog";

    protected ProductQueryModel QueryModel;

    protected List<Guid> FavoriteProducts = new();

    protected int PageCount { get; set; }
    
    protected int Page { get; set; } = 1;
    protected int PageSize { get; set; } = 4;

    protected string SortOrder = "desc";
    protected string? SortColumn = string.Empty;
    protected int? PriceFrom;
    protected int? PriceTo;
    
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

    protected override async Task OnInitializedAsync()
    {
        SortItem = SortItems[0];
        var result = await AccountService.GetAllFavoriteAsync();
        
        if (result.IsSuccess && result.Value != null)
        {
            FavoriteProducts = result.Value.ToList();
        }
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

        if (query.TryGetValue("priceProm", out var priceFrom))
        {
            QueryModel.PriceFrom = int.Parse(priceFrom);
        }
        
        if (query.TryGetValue("priceTo", out var priceTo))
        {
            QueryModel.PriceTo = int.Parse(priceTo);
        }
        
        await ReloadData(QueryModel);
    }

    private async Task ReloadData(ProductQueryModel queryModel)
    {
        IsLoading = true;
        var result = await ProductService.GetProductsByQueryAsync(queryModel);
            
        if (result.IsSuccess && result.Value != null)
        {
            PagedList = result.Value;
            PageCount = (int)Math.Ceiling((double)PagedList.TotalCount / PageSize);
            StateHasChanged();
            IsLoading = false;
            if (PriceFrom == 0)
            {
                PriceFrom = PagedList.MinPrice;
                PriceTo = PagedList.MaxPrice;
            }
        }
    }
    
    protected async Task SelectedPage(int value)
    {
        Page = value;
        await SendAsync();
    }

    protected async Task SelectedSort(SortItem sortValue)
    {
        var values = sortValue.Value.Split(',');
        SortColumn = values[0];
        SortOrder = values[1];
        SortItem = sortValue;
        await SendAsync();
    }
    
    public void NotifyChanged()
    {
        InvokeAsync(StateHasChanged);
    }

    protected async Task UpperPriceChanged(int value)
    {
        await PriceChanged(value);
    }

    protected async Task LowerPriceChanged(int value)
    {
        await PriceChanged(value, false);
    }

    protected async Task PriceChanged(int value, bool isUpperPrice = true)
    {
        if (isUpperPrice)
        {
            PriceTo = value;
        }
        else
        {
            PriceFrom = value;
        }
        
        await SendAsync();
    }

    protected async Task SendAsync(PriceChangeModel? model = null)
    {
        if (model is not null)
        {
            PriceFrom = model.PriceFrom;
            PriceTo = model.PriceTo;
        }
        
        QueryModel = new ProductQueryModel
        {
            Page = Page,
            PageSize = PageSize,
            SortColumn = SortColumn,
            SortOrder = SortOrder,
            PriceFrom = PriceFrom,
            PriceTo = PriceTo,
            CatalogName = CatalogName
        };
        QueryModel.CatalogName = CatalogName;
        var query = GetQueryUrl(Path);
        NavigationManager.NavigateTo($"{Path}/{CatalogName}{query}");
        await ReloadData(QueryModel);
        ShouldRender();
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
            queryUrl = queryUrl.AddParameter("priceTo", PriceTo.ToString());
    
        return queryUrl.Query;
    }
}

public class SortItem(string name, string value)
{
    public string Name { get; } = name;

    public string Value { get; } = value;

    public override bool Equals(object? obj)
    {
        var other = obj as SortItem;
        return other?.Name == Name;
    }

    public override string ToString() => Name;

    public override int GetHashCode() => Name.GetHashCode();
}