using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetAllCatalogs;

internal sealed class GetAllCatalogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IQueryHandler<GetAllCatalogsQuery, IEnumerable<CatalogModel>>
{
    public async Task<Result<IEnumerable<CatalogModel>>> Handle(
        GetAllCatalogsQuery request, 
        CancellationToken cancellationToken)
    {
        var catalog = await unitOfWork.CatalogRepository.GetAllAsync(cancellationToken);
        
        return Result<IEnumerable<CatalogModel>>.Success(mapper.Map<IEnumerable<CatalogModel>>(catalog));
    }
}