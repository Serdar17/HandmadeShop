using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Commands.UploadImages;

public sealed record UploadImagesCommand(Guid ProductId, UploadImagesModel Model) : ICommand<UploadedImageModel>;