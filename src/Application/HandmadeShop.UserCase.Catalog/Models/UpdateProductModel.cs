using AutoMapper;
using HandmadeShop.Domain;

namespace HandmadeShop.UserCase.Catalog.Models;

public class UpdateProductModel
{
    /// <summary>
    /// Unique product id
    /// </summary>
    public Guid Uid { get; set; }
    
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
    /// <example>false</example>
    public bool HasDiscount { get; set; }
    
    /// <summary>
    /// Discount percentage if has discount
    /// </summary>
    /// <example>null</example>
    public decimal? DiscountPercentage { get; set; }
    
    /// <summary>
    /// Catalog name
    /// </summary>
    public required string CatalogName { get; set; }
}

public class UpdateProductModelProfile : Profile
{
    public UpdateProductModelProfile()
    {
        CreateMap<UpdateProductModel, Product>()
            .ForMember(x => x.Uid, opt => opt.Ignore());
    }
}