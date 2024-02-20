namespace HandmadeShop.SharedModel.Catalogs.Models;

public class ProductQueryModel
{
    /// <summary>
    /// The search value
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// Sort column
    /// </summary>
    public string? SortColumn { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    public string? SortOrder { get; set; }

    /// <summary>
    /// Number of page
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    public string? CatalogName { get; set; }
    
    public int? PriceFrom { get; set; }

    public int? PriceTo { get; set; }

    public bool IsFavorite { get; set; }
}