using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;

namespace HandmadeShop.UserCase.Catalog.Commands.DeleteProduct;

internal sealed class DeleteProductHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);
        
        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        await unitOfWork.ProductRepository.DeleteByIdAsync(request.ProductId, cancellationToken);
        await cacheService.RemoveAsync($"product-{request.ProductId}", cancellationToken);
        
        return Result.Success();
    }
}