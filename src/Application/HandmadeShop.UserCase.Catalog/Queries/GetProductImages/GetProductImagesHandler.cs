using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProductImages;

public class GetProductImagesHandler : IQueryHandler<GetProductImagesQuery, IEnumerable<ProductImageModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;

    public GetProductImagesHandler(IUnitOfWork unitOfWork, IFileStorage fileStorage)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
    }

    public async Task<Result<IEnumerable<ProductImageModel>>> Handle(GetProductImagesQuery request, 
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        var images = new List<ProductImageModel>();

        await UploadImagesAsync(images, product);

        return images;
    }

    private async Task UploadImagesAsync(List<ProductImageModel> images, Product product)
    {
        foreach (var image in product.Images)
        {
            var path = await _fileStorage.GetDownloadLinkAsync(image);
            images.Add(new ProductImageModel(image, path));
        }
    }
}