using AutoMapper;
using FluentValidation;
using HandmadeShop.UseCase.Account.Models;

namespace HandmadeShop.Api.Controllers.Account.Models;

/// <summary>
/// Upload avatar request
/// </summary>
public class UploadAvatarRequest
{
    /// <summary>
    /// Avatar
    /// </summary>
    public required IFormFile Avatar { get; set; }
}

/// <summary>
/// Validation rules for <see cref="UploadAvatarRequest"/>
/// </summary>
public class UploadAvatarRequestValidator : AbstractValidator<UploadAvatarRequest>
{
    /// <summary>
    /// Ctor for <see cref="UploadAvatarRequestValidator"/>
    /// </summary>
    public UploadAvatarRequestValidator()
    {
        RuleFor(x => x.Avatar)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="UploadAvatarRequestProfile"/>
/// </summary>
public class UploadAvatarRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="UploadAvatarRequestProfile"/>
    /// </summary>
    public UploadAvatarRequestProfile()
    {
        CreateMap<UploadAvatarRequest, UploadAvatarModel>();
    }
}