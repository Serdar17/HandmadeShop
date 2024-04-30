using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProductInfo;

internal sealed class GetProductInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IQueryHandler<GetProductInfoQuery, ProductInfoModel>
{
    public async Task<Result<ProductInfoModel>> Handle(GetProductInfoQuery request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        return mapper.Map<ProductInfoModel>(product);
    }
}