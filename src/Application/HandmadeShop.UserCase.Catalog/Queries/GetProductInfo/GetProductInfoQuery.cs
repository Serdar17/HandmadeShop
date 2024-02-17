using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProductInfo;

public sealed record GetProductInfoQuery(Guid ProductId) : IQuery<ProductInfoModel>;