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

internal sealed class UpdateProductHandler : ICommandHandler<UpdateProductCommand, ProductModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly ICacheService _cacheService;

    public UpdateProductHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper, 
        IPublisher publisher,
        ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _publisher = publisher;
        _cacheService = cacheService;
    }

    public async Task<Result<ProductModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"product-{request.Model.Uid}";
        var data = await _cacheService.GetAsync<ProductModel>(cacheKey, cancellationToken: cancellationToken);

        if (data is not null)
        {
            return data;
        }
        
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Model.Uid, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.Model.Uid);
        }

        _mapper.Map(request.Model, product);

        var catalog = await _unitOfWork.CatalogRepository.GetByNameAsync(request.Model.CatalogName);
        
        if (catalog is null)
        {
            product.Catalog = new Domain.Catalog(request.Model.CatalogName);
        }
        else
        {
            product.Catalog = catalog;
        }

        await _unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        data = _mapper.Map<ProductModel>(product);
        await _publisher.Publish(new ProductPriceChangedEvent(data), cancellationToken);

        await _cacheService.PutAsync(cacheKey, data, TimeSpan.FromHours(1), cancellationToken);

        return data;
    }
}