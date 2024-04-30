using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;

namespace HandmadeShop.UserCase.Catalog.Commands.DeleteProductImage;

public class DeleteProductImageHandler(
    IUnitOfWork unitOfWork,
    IFileStorage fileStorage,
    ICacheService cacheService)
    : ICommandHandler<DeleteProductImageCommand>
{
    public async Task<Result> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        await fileStorage.DeleteFileAsync(request.Model.PathToImage, cancellationToken);

        product.Images.Remove(request.Model.PathToImage);
        
        var cacheKey = $"product-{product.Uid}";
        await cacheService.RemoveAsync(cacheKey, cancellationToken);

        await unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}