using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Account.Commands.UpdateAccountInfo;

internal sealed class UpdateAccountInfoHandler : ICommandHandler<UpdateAccountInfoCommand, AccountInfoModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly ICacheService _cacheService;

    public UpdateAccountInfoHandler(
        UserManager<User> userManager, 
        IMapper mapper, 
        IIdentityService identityService, 
        ICacheService cacheService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
        _cacheService = cacheService;
    }

    public async Task<Result<AccountInfoModel>> Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var cacheKey = $"user-info-{userId}";
        await _cacheService.RemoveAsync(cacheKey, cancellationToken);

        _mapper.Map(request.Model, user);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.UpdateError(string.Join(", ", result.Errors.Select(s => s.Description)));
        }

        var data = _mapper.Map<AccountInfoModel>(user);
        await _cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);

        return data;
    }
}