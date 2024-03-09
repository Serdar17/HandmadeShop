using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.UseCase.Order.Commands.ConfirmOrder;

public sealed record ConfirmOrderCommand(ConfirmOrderModel Model) : ICommand<OrderModel>;