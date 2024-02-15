using AutoMapper;
using HandmadeShop.Domain;
using Microsoft.AspNetCore.Http;

namespace HandmadeShop.UserCase.Catalog.Models;

public class CreateProductModel
{
    /// <summary>
    /// Product name
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Product description
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Quantity of product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Product price
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Has discount
    /// </summary>
    public bool HasDiscount { get; set; }
    
    /// <summary>
    /// Discount percentage if has discount
    /// </summary>
    public decimal? DiscountPercentage { get; set; }
    
    /// <summary>
    /// Catalog name
    /// </summary>
    public required string CatalogName { get; set; }
}

public class CreateProductModelProfile : Profile
{
    public CreateProductModelProfile()
    {
        CreateMap<CreateProductModel, Product>();
    }
}