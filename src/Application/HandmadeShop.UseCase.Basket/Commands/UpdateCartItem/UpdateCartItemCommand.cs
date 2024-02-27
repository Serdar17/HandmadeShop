using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.UseCase.Basket.Commands.UpdateCartItem;

public sealed record UpdateCartItemCommand(UpdateCartItemModel Model) : ICommand<CartItemModel>;