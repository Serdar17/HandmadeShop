using System.Linq.Expressions;
using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.UserCase.Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProducts;

public class GetProductsHandler : IQueryHandler<GetProductsQuery, PagedList<ProductModel>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<ProductModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var productQuery = _context.Products
            .Include(x => x.Catalog)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.Query.CatalogName))
        {
            productQuery = productQuery
                .Where(x => x.Catalog.Name.ToLower().Equals(request.Query.CatalogName.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(request.Query.Search))
        {
            productQuery = productQuery.Where(x => x.Name.Contains(request.Query.Search));
        }

        if (request.Query.SortOrder?.ToLower() == "desc")
        {
            productQuery = productQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            productQuery = productQuery.OrderBy(GetSortProperty(request));
        }

        var productResponseQuery = productQuery
            .Select(x => _mapper.Map<ProductModel>(x));

        var products = await 
            PagedList<ProductModel>.CreateAsync(productResponseQuery, request.Query.Page, request.Query.PageSize);
        
        return products;
    }

    private static Expression<Func<Product, object>> GetSortProperty(GetProductsQuery request)
    {
        return request.Query.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "date" => product => product.CreatedAt,
            "popular" => product => product.Price,
            "price" => product => product.Price,
            _ => product => product.Id
        };
    }
}