using AutoMapper;
using FluentValidation;
using HandmadeShop.UseCase.Review.Models;

namespace HandmadeShop.Api.Controllers.Review.Models;

/// <summary>
/// Upload review image request
/// </summary>
public class UploadReviewImageRequest
{
    /// <summary>
    /// Review id
    /// </summary>
    public Guid ReviewId { get; set; }
    
    /// <summary>
    /// Upload images
    /// </summary>
    public required IFormFile Image { get; set; }
}

// TODO: Заинжектить main настройки в ctor и валидирвоать размер файла из настроек

/// <summary>
/// Valiadtion rules for <see cref="UploadReviewImageRequest"/>
/// </summary>
public class UploadReviewImageRequestValidator : AbstractValidator<UploadReviewImageRequest>
{
    /// <summary>
    /// Ctor for <see cref="UploadReviewImageRequestValidator"/>
    /// </summary>
    public UploadReviewImageRequestValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotEmpty();
        
        // RuleFor(x => x.Image)
        //     .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="UploadReviewImageRequestProfile"/>
/// </summary>
public class UploadReviewImageRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="UploadReviewImageRequestProfile"/>
    /// </summary>
    public UploadReviewImageRequestProfile()
    {
        CreateMap<UploadReviewImageRequest, UploadReviewImageModel>();
    }
}