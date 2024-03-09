using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.UseCase.Order.Commands.CancelOrder;

public sealed record CancelOrderCommand(CancelOrderModel Model) : ICommand<OrderModel>;