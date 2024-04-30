using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Common.Constants;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Commands.UploadImages;

internal sealed class UploadImagesHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IFileStorage fileStorage,
    ICacheService cacheService)
    : ICommandHandler<UploadImagesCommand, UploadedImageModel>
{
    private readonly IMapper _mapper = mapper;

    public async Task<Result<UploadedImageModel>> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        var cacheKey = $"product-{product.Uid}";
        await cacheService.RemoveAsync(cacheKey, cancellationToken);

        var path = await fileStorage.UploadAsync(
            product.Uid,
            request.Model.Image,
            FolderPaths.PathToProductImagesFolder, cancellationToken);
            
        product.Images.Add(path);
        
        await unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var model = new UploadedImageModel()
        {
            ImagePath = path,
            DownloadUrl = await fileStorage.GetDownloadLinkAsync(path, cancellationToken)
        };
        
        return model;
    }
}