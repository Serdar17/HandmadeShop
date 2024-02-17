using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProductInfo;

public sealed record GetProductInfoQuery(Guid ProductId) : IQuery<ProductInfoModel>;