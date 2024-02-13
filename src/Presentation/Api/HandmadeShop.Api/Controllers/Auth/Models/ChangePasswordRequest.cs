using AutoMapper;
using FluentValidation;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.Api.Controllers.Auth.Models;

/// <summary>
/// Change password request
/// </summary>
public class ChangePasswordRequest
{
    /// <summary>
    /// Email
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Old password
    /// </summary>
    public required string OldPassword { get; set; }

    /// <summary>
    /// New password
    /// </summary>
    public required string NewPassword { get; set; }
}

/// <summary>
/// Validation rules for <see cref="ChangePasswordRequest"/>
/// </summary>
public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    /// <summary>
    /// Ctor for <see cref="ChangePasswordRequestValidator"/>
    /// </summary>
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.OldPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="ChangePasswordRequest"/>
/// </summary>
public class ChangePasswordRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="ChangePasswordRequestProfile"/>
    /// </summary>
    public ChangePasswordRequestProfile()
    {
        CreateMap<ChangePasswordRequest, ChangePasswordModel>();
    }
}