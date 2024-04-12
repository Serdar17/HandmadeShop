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

internal sealed class UploadAvatarHandler : ICommandHandler<UploadAvatarCommand, AccountInfoModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IIdentityService _identityService;
    private readonly IFileStorage _fileStorage;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public UploadAvatarHandler(
        UserManager<User> userManager, 
        IIdentityService identityService, 
        IFileStorage fileStorage,
        IMapper mapper,
        ICacheService cacheService)
    {
        _userManager = userManager;
        _identityService = identityService;
        _fileStorage = fileStorage;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<Result<AccountInfoModel>> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var cacheKey = $"user-info-{userId}";
        await _cacheService.RemoveAsync(cacheKey, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }
        
        var path = await _fileStorage.UploadAsync(
            user.Id, 
            request.Model.Avatar, 
            FolderPaths.PathToAvatarsFolder, 
            cancellationToken);
        
        user.AvatarUrl = path;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.CreateError(string.Join(", ", result.Errors.Select(s => s.Description)));
        }

        var data = _mapper.Map<AccountInfoModel>(user);
        await _cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);
  
        return data;
    }
}