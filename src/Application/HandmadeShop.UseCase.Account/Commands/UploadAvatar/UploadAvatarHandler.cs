using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Common.Constants;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Account.Commands.UploadAvatar;

internal sealed class UploadAvatarHandler(
    UserManager<User> userManager,
    IIdentityService identityService,
    IFileStorage fileStorage,
    IMapper mapper,
    ICacheService cacheService)
    : ICommandHandler<UploadAvatarCommand, AccountInfoModel>
{
    public async Task<Result<AccountInfoModel>> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        var cacheKey = $"user-info-{userId}";
        await cacheService.RemoveAsync(cacheKey, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }
        
        var path = await fileStorage.UploadAsync(
            user.Id, 
            request.Model.Avatar, 
            FolderPaths.PathToAvatarsFolder, 
            cancellationToken);
        
        user.AvatarUrl = path;
        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.CreateError(string.Join(", ", result.Errors.Select(s => s.Description)));
        }

        var data = mapper.Map<AccountInfoModel>(user);
        await cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);
  
        return data;
    }
}