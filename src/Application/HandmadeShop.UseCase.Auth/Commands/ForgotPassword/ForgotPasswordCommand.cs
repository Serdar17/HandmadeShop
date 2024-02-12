using HandmadeShop.UseCase.Auth.Models;
using ICommand = HandmadeShop.Application.Abstraction.Messaging.ICommand;

namespace HandmadeShop.UseCase.Auth.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(ForgotPasswordModel Model) : ICommand;