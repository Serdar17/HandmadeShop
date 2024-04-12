using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
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
    private readonly ICacheService _cacheService;

    public DeleteAvatarHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IIdentityService identityService, 
        IFileStorage fileStorage, 
        ICacheService cacheService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
        _fileStorage = fileStorage;
        _cacheService = cacheService;
    }

    public async Task<Result<AccountInfoModel>> Handle(DeleteAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var cacheKey = $"user-info-{userId}";
        await _cacheService.RemoveAsync(cacheKey, cancellationToken);

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

        var data = _mapper.Map<AccountInfoModel>(user);
        await _cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);

        return data;
    }
}