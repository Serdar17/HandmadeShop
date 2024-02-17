using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Commands.CreateProduct;

public sealed record CreateProductCommand(CreateProductModel Model) : ICommand<ProductModel>;