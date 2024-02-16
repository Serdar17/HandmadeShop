using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;

namespace HandmadeShop.UserCase.Catalog.Commands.DeleteProduct;

internal sealed class DeleteProductHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);
        
        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        await _unitOfWork.ProductRepository.DeleteByIdAsync(request.ProductId, cancellationToken);
        
        return Result.Success();
    }
}