using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.UserCase.Catalog.Models;

namespace HandmadeShop.UserCase.Catalog.Queries.GetAllCatalogs;

internal sealed class GetAllCatalogsHandler : IQueryHandler<GetAllCatalogsQuery, IEnumerable<CatalogModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCatalogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CatalogModel>>> Handle(
        GetAllCatalogsQuery request, 
        CancellationToken cancellationToken)
    {
        var catalog = await _unitOfWork.CatalogRepository.GetAllAsync(cancellationToken);
        
        return Result<IEnumerable<CatalogModel>>.Success(_mapper.Map<IEnumerable<CatalogModel>>(catalog));
    }
}