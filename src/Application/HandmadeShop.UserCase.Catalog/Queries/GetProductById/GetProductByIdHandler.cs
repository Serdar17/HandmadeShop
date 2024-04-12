using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProductById;

internal class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, ProductModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetProductByIdHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<Result<ProductModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"product-{request.ProductId}";
        var data = await _cacheService.GetAsync<ProductModel>(cacheKey, cancellationToken: cancellationToken);

        if (data is not null)
        {
            return data;
        }
        
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        data = _mapper.Map<ProductModel>(product);
        await _cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);
        return data;
    }
}