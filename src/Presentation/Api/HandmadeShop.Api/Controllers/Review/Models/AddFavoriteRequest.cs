using AutoMapper;
using FluentValidation;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.Api.Controllers.Review.Models;

/// <summary>
/// Add favorite request
/// </summary>
public class AddFavoriteRequest
{
    /// <summary>
    /// Product id
    /// </summary>
    public Guid ProductId { get; set; }
}

/// <summary>
/// Validation rules for <see cref="AddFavoriteRequest"/>
/// </summary>
public class AddFavoriteRequestValidator : AbstractValidator<AddFavoriteRequest>
{
    /// <summary>
    /// Ctor for <see cref="AddFavoriteRequestValidator"/>
    /// </summary>
    public AddFavoriteRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="AddFavoriteRequest"/>
/// </summary>
public class AddFavoriteRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="AddFavoriteRequestProfile"/>
    /// </summary>
    public AddFavoriteRequestProfile()
    {
        CreateMap<AddFavoriteRequest, AddFavoriteModel>();
    }
}