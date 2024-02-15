using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Commands.DeleteProductImage;

public sealed record DeleteProductImageCommand(Guid ProductId, DeleteProductImageModel Model) : ICommand;