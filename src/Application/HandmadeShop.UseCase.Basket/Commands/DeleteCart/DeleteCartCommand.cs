using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.UseCase.Basket.Commands.DeleteCart;

public sealed record DeleteCartCommand(Guid ProductId) : ICommand<BasketModel>;