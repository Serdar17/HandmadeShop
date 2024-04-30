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

internal sealed class DeleteAvatarHandler(
    UserManager<User> userManager,
    IMapper mapper,
    IIdentityService identityService,
    IFileStorage fileStorage,
    ICacheService cacheService)
    : ICommandHandler<DeleteAvatarCommand, AccountInfoModel>
{
    public async Task<Result<AccountInfoModel>> Handle(DeleteAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        var cacheKey = $"user-info-{userId}";
        await cacheService.RemoveAsync(cacheKey, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        if (user.AvatarUrl is not null)
        {
            await fileStorage.DeleteFileAsync(user.AvatarUrl, cancellationToken);
            user.AvatarUrl = null;
        }

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.UpdateError(string.Join(", ", result.Errors.Select(x => x.Description)));
        }

        var data = mapper.Map<AccountInfoModel>(user);
        await cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);

        return data;
    }
}