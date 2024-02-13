using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.UseCase.Auth.Commands.ChangePassword;

public sealed record ChangePasswordCommand(ChangePasswordModel Model) : ICommand;