using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.UseCase.Order.Queries.GetOrders;

public sealed record GetOrdersQuery : IQuery<IEnumerable<OrderModel>>;