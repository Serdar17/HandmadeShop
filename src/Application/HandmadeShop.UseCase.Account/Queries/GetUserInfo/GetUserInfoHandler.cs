using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Account.Queries.GetUserInfo;

internal sealed class GetUserInfoHandler : IQueryHandler<GetUserInfoQuery, AccountInfoModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetUserInfoHandler(
        UserManager<User> userManager,
        ICacheService cacheService,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<Result<AccountInfoModel>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"user-info-{request.UserId}";
        var data = await _cacheService.GetAsync<AccountInfoModel>(cacheKey, cancellationToken: cancellationToken);

        if (data is not null)
        {
            return data;
        }
        
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(request.UserId);
        }

        data = _mapper.Map<AccountInfoModel>(user);
        await _cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);
        return data;
    }
}