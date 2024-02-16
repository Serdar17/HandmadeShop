using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProductImages;

public sealed record GetProductImagesQuery(Guid ProductId) : IQuery<IEnumerable<ProductImageModel>>;