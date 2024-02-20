using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Accounts.Models;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.UseCase.Account.Models;

namespace HandmadeShop.UseCase.Account.Queries.GetMyProducts;

public sealed record GetMyProductQuery(ProductQueryModel Model) : IQuery<PagedList<ProductModel>>;