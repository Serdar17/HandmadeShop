using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.UseCase.Auth.Commands.ResetProfilePassword;

public sealed record ResetProfilePasswordCommand(ResetProfilePasswordModel Model) : ICommand;