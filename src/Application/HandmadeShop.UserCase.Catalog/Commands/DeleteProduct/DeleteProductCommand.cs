using HandmadeShop.Application.Abstraction.Messaging;

namespace HandmadeShop.UserCase.Catalog.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand;