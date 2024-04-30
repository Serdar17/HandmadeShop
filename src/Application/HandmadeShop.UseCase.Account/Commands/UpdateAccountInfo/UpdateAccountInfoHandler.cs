using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Account.Commands.UpdateAccountInfo;

internal sealed class UpdateAccountInfoHandler(
    UserManager<User> userManager,
    IMapper mapper,
    IIdentityService identityService,
    ICacheService cacheService)
    : ICommandHandler<UpdateAccountInfoCommand, AccountInfoModel>
{
    public async Task<Result<AccountInfoModel>> Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var cacheKey = $"user-info-{userId}";
        await cacheService.RemoveAsync(cacheKey, cancellationToken);

        mapper.Map(request.Model, user);

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.UpdateError(string.Join(", ", result.Errors.Select(s => s.Description)));
        }

        var data = mapper.Map<AccountInfoModel>(user);
        await cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);

        return data;
    }
}