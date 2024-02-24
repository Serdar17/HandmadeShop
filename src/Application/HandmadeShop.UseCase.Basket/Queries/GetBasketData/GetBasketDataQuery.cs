using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.UseCase.Basket.Queries.GetBasketData;

public sealed record GetBasketDataQuery : IQuery<CartModel>;