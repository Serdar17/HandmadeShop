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

internal sealed class UploadImagesHandler : ICommandHandler<UploadImagesCommand, UploadedImageModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public UploadImagesHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IFileStorage fileStorage,
        ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorage = fileStorage;
        _cacheService = cacheService;
    }

    public async Task<Result<UploadedImageModel>> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        var cacheKey = $"product-{product.Uid}";
        await _cacheService.RemoveAsync(cacheKey, cancellationToken);

        var path = await _fileStorage.UploadAsync(
            product.Uid,
            request.Model.Image,
            FolderPaths.PathToProductImagesFolder, cancellationToken);
            
        product.Images.Add(path);
        
        await _unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var model = new UploadedImageModel()
        {
            ImagePath = path,
            DownloadUrl = await _fileStorage.GetDownloadLinkAsync(path, cancellationToken)
        };
        
        return model;
    }
}