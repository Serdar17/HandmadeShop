using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Commands.UpdateProduct;

public sealed record UpdateProductCommand(UpdateProductModel Model) : ICommand<ProductModel>;