using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UseCase.Account.Models;

namespace HandmadeShop.UseCase.Account.Commands.UpdateAccountInfo;

public sealed record UpdateAccountInfoCommand(UpdateAccountInfoModel Model) : ICommand<AccountInfoModel>;