using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.UseCase.Order.Queries.GetOrderDetails;

public sealed record GetOrderDetailsQuery(Guid OrderId) : IQuery<OrderModel>;