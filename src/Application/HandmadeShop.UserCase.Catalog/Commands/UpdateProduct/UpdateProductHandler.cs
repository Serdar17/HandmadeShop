using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Commands.UpdateProduct;

internal sealed class UpdateProductHandler : ICommandHandler<UpdateProductCommand, ProductModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ProductModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
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


        return _mapper.Map<ProductModel>(product);
    }
}