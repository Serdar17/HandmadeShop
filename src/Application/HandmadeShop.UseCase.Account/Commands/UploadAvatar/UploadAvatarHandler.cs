using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Common.Constants;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
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

    public UploadAvatarHandler(
        UserManager<User> userManager, 
        IIdentityService identityService, 
        IFileStorage fileStorage, IMapper mapper)
    {
        _userManager = userManager;
        _identityService = identityService;
        _fileStorage = fileStorage;
        _mapper = mapper;
    }

    public async Task<Result<AccountInfoModel>> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

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
  
        return _mapper.Map<AccountInfoModel>(user);
    }
}