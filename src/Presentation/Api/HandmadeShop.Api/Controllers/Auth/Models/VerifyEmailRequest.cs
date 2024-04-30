using AutoMapper;
using FluentValidation;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.Api.Controllers.Auth.Models;

/// <summary>
/// Verify email request
/// </summary>
public class VerifyEmailRequest
{
    /// <summary>
    /// Unique user id
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Email verification token
    /// </summary>
    public required string Token { get; set; }
}

/// <summary>
/// Validation rules for <see cref="VerifyEmailRequestValidator"/>
/// </summary>
public class VerifyEmailRequestValidator : AbstractValidator<VerifyEmailRequest>
{
    /// <summary>
    /// Ctor for <see cref="VerifyEmailRequestValidator"/>
    /// </summary>
    public VerifyEmailRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Token)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="VerifyEmailRequest"/>
/// </summary>
public class VerifyEmailRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="VerifyEmailRequestProfile"/>
    /// </summary>
    public VerifyEmailRequestProfile()
    {
        CreateMap<VerifyEmailRequest, VerifyEmailModel>();
    }
}

