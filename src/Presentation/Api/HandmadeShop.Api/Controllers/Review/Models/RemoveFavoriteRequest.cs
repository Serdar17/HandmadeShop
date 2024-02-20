using AutoMapper;
using FluentValidation;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.Api.Controllers.Review.Models;

/// <summary>
/// Remove favorite request
/// </summary>
public class RemoveFavoriteRequest
{
    /// <summary>
    /// Product id
    /// </summary>
    public Guid ProductId { get; set; }
}

/// <summary>
/// Validation rules for <see cref="RemoveFavoriteRequest"/>
/// </summary>
public class RemoveFavoriteRequestValidator : AbstractValidator<RemoveFavoriteRequest>
{
    /// <summary>
    /// Ctor for <see cref="RemoveFavoriteRequestValidator"/>
    /// </summary>
    public RemoveFavoriteRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="RemoveFavoriteRequest"/>
/// </summary>
public class RemoveFavoriteRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="RemoveFavoriteRequestProfile"/>
    /// </summary>
    public RemoveFavoriteRequestProfile()
    {
        CreateMap<RemoveFavoriteRequest, RemoveFavoriteModel>();
    }
}