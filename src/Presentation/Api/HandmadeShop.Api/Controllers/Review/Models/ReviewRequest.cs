using AutoMapper;
using FluentValidation;
using HandmadeShop.Common.ValidationRules;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.Api.Controllers.Review.Models;

/// <summary>
/// Review request
/// </summary>
public class ReviewRequest
{
    /// <summary>
    /// Unique product id
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Comment
    /// </summary>
    public required string Comment { get; set; }
    
    /// <summary>
    /// Rating
    /// </summary>
    public int Rating { get; set; }
}

/// <summary>
/// Validation rules for <see cref="ReviewRequest"/>
/// </summary>
public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
{
    /// <summary>
    /// Ctor for <see cref="ReviewRequestValidator"/>
    /// </summary>
    public ReviewRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
        
        RuleFor(x => x.Comment)
            .NotEmpty();

        RuleFor(x => x.Rating)
            .Must(x => x is > 0 and < 6)
            .WithMessage("Rating must be between 1 and 5");
    }
}

/// <summary>
/// Mapping rules for <see cref="ReviewRequest"/>
/// </summary>
public class ReviewRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="ReviewRequestProfile"/>
    /// </summary>
    public ReviewRequestProfile()
    {
        CreateMap<ReviewRequest, ReviewModel>();
    }
}
