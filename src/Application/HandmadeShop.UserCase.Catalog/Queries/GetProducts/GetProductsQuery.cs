using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProducts;

public sealed record GetProductsQuery(ProductQueryModel Query) : IQuery<PagedList<ProductModel>>;