using HandmadeShop.SharedModel.Catalogs.Models;
using MediatR;

namespace HandmadeShop.Application.Events;

public sealed record ProductPriceChangedEvent(ProductModel Model) : INotification;