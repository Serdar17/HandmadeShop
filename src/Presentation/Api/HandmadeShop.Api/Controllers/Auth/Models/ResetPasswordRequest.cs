using AutoMapper;
using FluentValidation;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.Api.Controllers.Auth.Models;

/// <summary>
/// Reset password request
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// User email
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Token
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// New password
    /// </summary>
    public required string Password { get; set; }
}

/// <summary>
/// Validation rules for <see cref="ResetPasswordRequest"/>
/// </summary>
public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    /// <summary>
    /// Ctor for <see cref="ResetPasswordRequestValidator"/>
    /// </summary>
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(1, 50);

        RuleFor(x => x.Token)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="ResetPasswordRequest"/>
/// </summary>
public class ResetPasswordRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="ResetPasswordRequestProfile"/>
    /// </summary>
    public ResetPasswordRequestProfile()
    {
        CreateMap<ResetPasswordRequest, ResetPasswordModel>();
    }
}