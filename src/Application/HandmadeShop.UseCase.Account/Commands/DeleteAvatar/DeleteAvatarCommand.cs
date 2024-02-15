using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UseCase.Account.Models;

namespace HandmadeShop.UseCase.Account.Commands.DeleteAvatar;

public sealed record DeleteAvatarCommand : ICommand<AccountInfoModel>;