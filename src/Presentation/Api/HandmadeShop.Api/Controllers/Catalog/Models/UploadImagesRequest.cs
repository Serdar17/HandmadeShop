using AutoMapper;
using FluentValidation;
using HandmadeShop.Common.ValidationRules;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.Api.Controllers.Catalog.Models;

/// <summary>
/// Upload product images
/// </summary>
public class UploadImagesRequest
{
    /// <summary>
    /// List of product images
    /// </summary>
    public required IFormFile Image { get; set; }
}

/// <summary>
/// Validation rules for <see cref="UploadImagesRequest"/>
/// </summary>
public class UploadImagesRequestValidator : AbstractValidator<UploadImagesRequest>
{
    /// <summary>
    /// Ctor for <see cref="UploadImagesRequestValidator"/>
    /// </summary>
    public UploadImagesRequestValidator()
    {
        RuleFor(x => x.Image)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="UploadImagesRequest"/>
/// </summary>
public class UploadImagesRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="UploadImagesRequestProfile"/>
    /// </summary>
    public UploadImagesRequestProfile()
    {
        CreateMap<UploadImagesRequest, UploadImagesModel>();
    }
}