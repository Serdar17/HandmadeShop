using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Account.Commands.DeleteAvatar;

internal sealed class DeleteAvatarHandler : ICommandHandler<DeleteAvatarCommand, AccountInfoModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly IFileStorage _fileStorage;

    public DeleteAvatarHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IIdentityService identityService, 
        IFileStorage fileStorage)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
        _fileStorage = fileStorage;
    }

    public async Task<Result<AccountInfoModel>> Handle(DeleteAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        if (user.AvatarUrl is not null)
        {
            await _fileStorage.DeleteFileAsync(user.AvatarUrl, cancellationToken);
            user.AvatarUrl = null;
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.UpdateError(string.Join(", ", result.Errors.Select(x => x.Description)));
        }

        return _mapper.Map<AccountInfoModel>(user);
    }
}