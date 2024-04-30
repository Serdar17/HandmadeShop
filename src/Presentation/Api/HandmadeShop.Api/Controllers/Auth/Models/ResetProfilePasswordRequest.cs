using AutoMapper;
using FluentValidation;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.Api.Controllers.Auth.Models;

/// <summary>
/// Reset profile password request
/// </summary>
public class ResetProfilePasswordRequest
{
    /// <summary>
    /// Password
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Confirm Password
    /// </summary>
    public required string ConfirmPassword { get; set; }
}

/// <summary>
/// Validation rules for <see cref="ResetProfilePasswordRequest"/>
/// </summary>
public class ResetProfilePasswordRequestValidator : AbstractValidator<ResetProfilePasswordRequest>
{
    /// <summary>
    /// Ctor for <see cref="ResetProfilePasswordRequestValidator"/>
    /// </summary>
    public ResetProfilePasswordRequestValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(1, 50);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);
    }
}

/// <summary>
/// Mapping rules for <see cref="ResetProfilePasswordRequest"/>
/// </summary>
public class ResetProfilePasswordRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="ResetProfilePasswordRequestProfile"/>
    /// </summary>
    public ResetProfilePasswordRequestProfile()
    {
        CreateMap<ResetProfilePasswordRequest, ResetProfilePasswordModel>();
    }
}