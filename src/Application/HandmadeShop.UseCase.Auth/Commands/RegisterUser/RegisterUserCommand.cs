using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain.Common;
using HandmadeShop.UseCase.Auth.Models;

namespace HandmadeShop.UseCase.Auth.Commands.RegisterUser;

public sealed record RegisterUserCommand(RegisterUserModel Model) : ICommand<UserAccountModel>;
