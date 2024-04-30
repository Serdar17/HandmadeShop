using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Application.Events;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.SharedModel.Catalogs.Models;
using MediatR;

namespace HandmadeShop.UserCase.Catalog.Commands.UpdateProduct;

internal sealed class UpdateProductHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IPublisher publisher,
    ICacheService cacheService)
    : ICommandHandler<UpdateProductCommand, ProductModel>
{
    public async Task<Result<ProductModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"product-{request.Model.Uid}";
        var data = await cacheService.GetAsync<ProductModel>(cacheKey, cancellationToken: cancellationToken);

        if (data is not null)
        {
            return data;
        }
        
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.Model.Uid, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.Model.Uid);
        }

        mapper.Map(request.Model, product);

        var catalog = await unitOfWork.CatalogRepository.GetByNameAsync(request.Model.CatalogName);
        
        if (catalog is null)
        {
            product.Catalog = new Domain.Catalog(request.Model.CatalogName);
        }
        else
        {
            product.Catalog = catalog;
        }

        await unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        data = mapper.Map<ProductModel>(product);
        await publisher.Publish(new ProductPriceChangedEvent(data), cancellationToken);

        await cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);

        return data;
    }
}