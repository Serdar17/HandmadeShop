using AutoMapper;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.Api.Controllers.Catalog.Models;

/// <summary>
/// Product query
/// </summary>
public class ProductQueryRequest
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

    /// <summary>
    /// Catalog Name
    /// </summary>
    public string? CatalogName { get; set; }
}

/// <summary>
/// Mapping rules for <see cref="ProductQueryRequest"/>
/// </summary>
public class ProductQueryRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="ProductQueryRequestProfile"/>
    /// </summary>
    public ProductQueryRequestProfile()
    {
        CreateMap<ProductQueryRequest, ProductQueryModel>();
    }
}