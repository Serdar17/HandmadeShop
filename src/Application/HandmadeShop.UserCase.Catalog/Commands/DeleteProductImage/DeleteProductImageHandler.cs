using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;

namespace HandmadeShop.UserCase.Catalog.Commands.DeleteProductImage;

public class DeleteProductImageHandler : ICommandHandler<DeleteProductImageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly ICacheService _cacheService;

    public DeleteProductImageHandler(
        IUnitOfWork unitOfWork, 
        IFileStorage fileStorage, 
        ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
        _cacheService = cacheService;
    }

    public async Task<Result> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        await _fileStorage.DeleteFileAsync(request.Model.PathToImage, cancellationToken);

        product.Images.Remove(request.Model.PathToImage);
        
        var cacheKey = $"product-{product.Uid}";
        await _cacheService.RemoveAsync(cacheKey, cancellationToken);

        await _unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}