using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Extensions;
using HandmadeShop.Web.Pages.Product.Models;
using HandmadeShop.Web.Pages.Product.Services;
using HandmadeShop.Web.Pages.Profile.Services;
using HandmadeShop.Web.TransferServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace HandmadeShop.Web.Pages.Product;

public class SearchBase : ComponentBase
{
    [Inject] protected IProductService ProductService { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; }
    protected ProductQueryModel QueryModel;
    protected PagedList<ProductModel>? PagedList;
    [Inject] protected IAccountService AccountService { get; set; }
    [Inject] protected SearchValueTransferService SearchValueTransferService { get; set; }
    
    protected int PageCount { get; set; }
    
    protected int Page { get; set; } = 1;
    protected int PageSize { get; set; } = 4;
    
    protected List<Guid> FavoriteProducts = new();

    protected string SortOrder = "desc";
    protected string? SortColumn = string.Empty;
    protected int? PriceFrom;
    protected int? PriceTo;
    protected string? SearchValue;
    protected SortItem SortItem;
    private const string Path = "/search";
    
    protected List<SortItem> SortItems = new()
    {
        new SortItem("Последние", "date,desc"),
        new SortItem("Популярные", "popular,desc"),
        new SortItem("Сначала дорогие", "price,desc"),
        new SortItem("Сначала дешевле", "price,asc"),
    };
    
    protected bool IsLoading { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        SortItem = SortItems[0];
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);
        QueryModel = new ProductQueryModel
        {
            PageSize = PageSize
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

        if (query.TryGetValue("searchValue", out var searchValue))
        {
            SearchValue = searchValue;
            QueryModel.Search = SearchValue;
        }
        
        await ReloadData(QueryModel);
    }

    protected override async Task OnInitializedAsync()
    {
        var result = await AccountService.GetAllFavoriteAsync();
        SearchValueTransferService.Data = SearchValue;
        
        if (result.IsSuccess && result.Value != null)
        {
            FavoriteProducts = result.Value.ToList();
        }
    }

    private async Task ReloadData(ProductQueryModel queryModel)
    {
        IsLoading = true;
        var result = await ProductService.GetProductsByQueryAsync(queryModel);
            
        if (result.IsSuccess && result.Value != null)
        {
            PagedList = result.Value;
            StateHasChanged();
        }
        
        IsLoading = false;
    }
    
    protected async Task SelectedSort(SortItem sortValue)
    {
        var values = sortValue.Value.Split(',');
        SortColumn = values[0];
        SortOrder = values[1];
        SortItem = sortValue;
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
            Search = SearchValue
        };
        var query = GetQueryUrl(Path);
        NavigationManager.NavigateTo($"{Path}{query}");
        await ReloadData(QueryModel);
        ShouldRender();
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
    
    protected async Task SelectedPage(int value)
    {
        Page = value;
        await SendAsync();
    }
    
    private string GetQueryUrl(string url)
    {
        var queryUrl = new Uri(url)
            .AddParameter("page", Page.ToString());

        if (!string.IsNullOrEmpty(SearchValue))
            queryUrl = queryUrl.AddParameter("searchValue", SearchValue);
    
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