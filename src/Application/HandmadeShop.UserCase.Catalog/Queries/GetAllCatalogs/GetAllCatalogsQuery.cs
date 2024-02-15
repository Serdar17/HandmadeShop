using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetAllCatalogs;

public sealed record GetAllCatalogsQuery : IQuery<IEnumerable<CatalogModel>>;