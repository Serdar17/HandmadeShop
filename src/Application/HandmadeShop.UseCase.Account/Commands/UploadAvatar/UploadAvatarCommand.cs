using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UseCase.Account.Models;

namespace HandmadeShop.UseCase.Account.Commands.UploadAvatar;

public sealed record UploadAvatarCommand(UploadAvatarModel Model) : ICommand<AccountInfoModel>;