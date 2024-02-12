using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.UseCase.Auth.Commands.VerifyEmail;

public sealed record VerifyEmailCommand(VerifyEmailModel Model) : ICommand;