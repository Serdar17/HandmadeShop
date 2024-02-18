using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.SharedModel.Catalogs.Models;

public class PagedList<T>
{
    public List<T> Items { get; }

    public int Page { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public int MaxPrice { get; set; }
    
    public int MinPrice { get; set; }

    public bool HasNextPage => PageSize * Page < TotalCount;

    public bool HasPreviousPage => Page > 1;

    public PagedList(List<T> items, int page, int pageSize, int totalCount, int maxPrice, int minPrice)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        MaxPrice = maxPrice;
        MinPrice = minPrice;
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize, int maxPrice, int minPrice)
    {
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new(items, page, pageSize, totalCount, maxPrice, minPrice);
    }

}