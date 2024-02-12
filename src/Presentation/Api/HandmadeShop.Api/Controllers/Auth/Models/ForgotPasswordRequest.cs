using AutoMapper;
using FluentValidation;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.Api.Controllers.Auth.Models;

/// <summary>
/// Forgot password request
/// </summary>
public class ForgotPasswordRequest
{
    /// <summary>
    /// Email
    /// </summary>
    public required string Email { get; set; }
}

/// <summary>
/// Validation rules for <see cref="ForgotPasswordRequest"/>
/// </summary>
public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    /// <summary>
    /// Ctor for <see cref="ForgotPasswordRequestValidator"/>
    /// </summary>
    public ForgotPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();
    }
}

/// <summary>
/// Mapping rule for <see cref="ForgotPasswordRequest"/>
/// </summary>
public class ForgotPasswordRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="ForgotPasswordRequestProfile"/>
    /// </summary>
    public ForgotPasswordRequestProfile()
    {
        CreateMap<ForgotPasswordRequest, ForgotPasswordModel>();
    }
}