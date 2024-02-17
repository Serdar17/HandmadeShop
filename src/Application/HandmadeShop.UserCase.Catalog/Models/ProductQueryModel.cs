namespace HandmadeShop.UserCase.Catalog.Models;

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
}