﻿using AutoMapper;
using FluentValidation;
using HandmadeShop.Common.ValidationRules;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.Api.Controllers.Catalog.Models;

/// <summary>
/// Update product request
/// </summary>
public class UpdateProductRequest
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

/// <summary>
/// Validation rules for <see cref="UpdateProductRequest"/>
/// </summary>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    /// <summary>
    /// Ctor for <see cref="UpdateProductRequestValidator"/>
    /// </summary>
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Uid)
            .NotEmpty();
        
        RuleFor(x => x.CatalogName)
            .CatalogName();

        RuleFor(x => x.Name)
            .ProductName();

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.DiscountPercentage)
            .NotEmpty()
            .When(x => x.HasDiscount)
            .GreaterThan(0);
    }
}

/// <summary>
/// Mapping rules for <see cref="UpdateProductRequest"/>
/// </summary>
public class UpdateProductRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="UpdateProductRequestProfile"/>
    /// </summary>
    public UpdateProductRequestProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductModel>();
    }
}