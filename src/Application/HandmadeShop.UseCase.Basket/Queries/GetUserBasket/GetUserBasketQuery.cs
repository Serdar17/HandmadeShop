using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.UseCase.Basket.Queries.GetUserBasket;

public sealed record GetUserBasketQuery : IQuery<BasketModel>;