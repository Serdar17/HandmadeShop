﻿using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.Web.Pages.Order.Services;

public interface IOrderService
{
    Task<Result<List<OrderModel>>> GetOrdersAsync();
    Task<Result<OrderModel>> GetOrderDetailsAsync(Guid orderId);
    Task<Result> CreateOrderAsync(CreateOrderModel model);
}