using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProductImages;

public class GetProductImagesHandler(IUnitOfWork unitOfWork, IFileStorage fileStorage)
    : IQueryHandler<GetProductImagesQuery, IEnumerable<ProductImageModel>>
{
    public async Task<Result<IEnumerable<ProductImageModel>>> Handle(GetProductImagesQuery request, 
        CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

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
            var path = await fileStorage.GetDownloadLinkAsync(image);
            images.Add(new ProductImageModel(image, path));
        }
    }
}