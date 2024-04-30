using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Account.Queries.GetUserInfo;

internal sealed class GetUserInfoHandler(
    UserManager<User> userManager,
    ICacheService cacheService,
    IMapper mapper)
    : IQueryHandler<GetUserInfoQuery, AccountInfoModel>
{
    public async Task<Result<AccountInfoModel>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"user-info-{request.UserId}";
        var data = await cacheService.GetAsync<AccountInfoModel>(cacheKey, cancellationToken: cancellationToken);

        if (data is not null)
        {
            return data;
        }
        
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(request.UserId);
        }

        data = mapper.Map<AccountInfoModel>(user);
        await cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);
        return data;
    }
}