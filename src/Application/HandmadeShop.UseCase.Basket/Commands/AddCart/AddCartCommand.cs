using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.UseCase.Basket.Commands.AddCart;

public sealed record AddCartCommand(AddCartModel Model) : ICommand<BasketModel>;