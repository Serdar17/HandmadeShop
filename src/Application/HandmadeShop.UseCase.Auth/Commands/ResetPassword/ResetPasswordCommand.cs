using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.UseCase.Auth.Commands.ResetPassword;

public sealed record ResetPasswordCommand(ResetPasswordModel Model) : ICommand;