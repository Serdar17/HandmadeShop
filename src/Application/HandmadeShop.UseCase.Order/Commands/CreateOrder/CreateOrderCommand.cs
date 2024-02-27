using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.UseCase.Order.Commands.CreateOrder;

public sealed record CreateOrderCommand(CreateOrderModel Model) : ICommand;