using AutoMapper;
using FluentValidation;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.Api.Controllers.Auth.Models;

/// <summary>
/// Model for User registration
/// </summary>
public class RegistrationUserRequest
{
    /// <summary>
    /// User name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public required string Password { get; set; }
}

/// <summary>
/// Validation rules for <see cref="RegistrationUserRequest"/>
/// </summary>
public class RegistrationUserRequestValidator : AbstractValidator<RegistrationUserRequest>
{
    /// <summary>
    /// Ctor for <see cref="RegistrationUserRequestValidator"/>
    /// </summary>
    public RegistrationUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="RegistrationUserRequest"/>
/// </summary>
public class RegistrationUserRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="RegistrationUserRequestProfile"/>
    /// </summary>
    public RegistrationUserRequestProfile()
    {
        CreateMap<RegistrationUserRequest, RegisterUserModel>();
    }
}