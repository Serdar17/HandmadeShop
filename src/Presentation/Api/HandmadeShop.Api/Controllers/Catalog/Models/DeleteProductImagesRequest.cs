using AutoMapper;
using FluentValidation;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.Api.Controllers.Catalog.Models;

/// <summary>
/// Delete product image request
/// </summary>
public class DeleteProductImagesRequest
{
    /// <summary>
    /// List of path to images
    /// </summary>
    public required string PathToImage { get; set; }
}

/// <summary>
/// Validation rules for <see cref="DeleteProductImagesRequest"/>
/// </summary>
public class DeleteProductImagesRequestValidator : AbstractValidator<DeleteProductImagesRequest>
{
    /// <summary>
    /// Ctor for <see cref="DeleteProductImagesRequestValidator"/>
    /// </summary>
    public DeleteProductImagesRequestValidator()
    {
        RuleFor(x => x.PathToImage)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="DeleteProductImagesRequest"/>
/// </summary>
public class DeleteProductImagesRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="DeleteProductImagesRequestProfile"/>
    /// </summary>
    public DeleteProductImagesRequestProfile()
    {
        CreateMap<DeleteProductImagesRequest, DeleteProductImageModel>();
    }
}