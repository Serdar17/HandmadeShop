using AutoMapper;
using FluentValidation;
using HandmadeShop.Domain.Enums;
using HandmadeShop.UseCase.Account.Models;

namespace HandmadeShop.Api.Controllers.Account.Models;

/// <summary>
/// Update Account info request
/// </summary>
public class UpdateAccountInfoRequest
{
    /// <summary>
    /// User name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gender
    /// </summary>
    public Gender Gender { get; set; } = Gender.Unknown;


    /// <summary>
    /// Birth Date
    /// </summary>
    public DateTime BirthDate { get; set; }
}

/// <summary>
/// Validation rules for <see cref="UpdateAccountInfoRequest"/>
/// </summary>
public class UpdateAccountInfoRequestValidator : AbstractValidator<UpdateAccountInfoRequest>
{
    /// <summary>
    /// Ctor for <see cref="UpdateAccountInfoRequestValidator"/>
    /// </summary>
    public UpdateAccountInfoRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Gender)
            .IsInEnum();

        RuleFor(x => x.BirthDate)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="UpdateAccountInfoRequest"/>
/// </summary>
public class UpdateAccountInfoRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="UpdateAccountInfoRequestProfile"/>
    /// </summary>
    public UpdateAccountInfoRequestProfile()
    {
        CreateMap<UpdateAccountInfoRequest, UpdateAccountInfoModel>();
    }
}