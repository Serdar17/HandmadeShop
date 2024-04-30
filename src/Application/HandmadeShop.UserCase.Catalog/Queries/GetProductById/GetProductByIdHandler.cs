using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProductById;

internal class GetProductByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService)
    : IQueryHandler<GetProductByIdQuery, ProductModel>
{
    public async Task<Result<ProductModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"product-{request.ProductId}";
        var data = await cacheService.GetAsync<ProductModel>(cacheKey, cancellationToken: cancellationToken);

        if (data is not null)
        {
            return data;
        }
        
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        data = mapper.Map<ProductModel>(product);
        await cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);
        return data;
    }
}