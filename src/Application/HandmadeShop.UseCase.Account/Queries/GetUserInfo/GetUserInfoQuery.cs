using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UseCase.Account.Models;

namespace HandmadeShop.UseCase.Account.Queries.GetUserInfo;

public sealed record GetUserInfoQuery(Guid UserId) : IQuery<AccountInfoModel>;