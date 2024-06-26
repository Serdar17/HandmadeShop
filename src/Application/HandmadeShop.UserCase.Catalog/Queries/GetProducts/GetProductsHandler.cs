﻿using System.Linq.Expressions;
using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.SharedModel.Catalogs.Models;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.UserCase.Catalog.Queries.GetProducts;

public class GetProductsHandler(IAppDbContext context, IMapper mapper)
    : IQueryHandler<GetProductsQuery, PagedList<ProductModel>>
{
    public async Task<Result<PagedList<ProductModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var productQuery = context.Products
            .Include(x => x.Catalog)
            .Include(x => x.Like)
            .Include(x => x.Reviews)
            .AsQueryable();

       if (!string.IsNullOrEmpty(request.Query.CatalogName))
        {
            productQuery = productQuery
                .Where(x => x.Catalog.Name.ToLower().Equals(request.Query.CatalogName.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(request.Query.Search))
        {
            productQuery = productQuery.Where(x => x.Name.ToLower().Contains(request.Query.Search.ToLower()));
        }
        
        var maxPrice = await productQuery.MaxAsync(x => x.Price);
        var minPrice = await productQuery.MinAsync(x => x.Price);

        if (request.Query.SortOrder?.ToLower() == "desc")
        {
            productQuery = productQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            productQuery = productQuery.OrderBy(GetSortProperty(request));
        }

        if (request.Query.PriceFrom != null)
        {
            productQuery = productQuery.Where(x => x.Price >= request.Query.PriceFrom);
        }

        if (request.Query.PriceTo != null)
        {
            productQuery = productQuery.Where(x => x.Price <= request.Query.PriceTo);
        }
        
        var productResponseQuery = productQuery
            .Select(x => mapper.Map<ProductModel>(x));

        var products = await 
            PagedList<ProductModel>.CreateAsync(
                productResponseQuery, 
                request.Query.Page,
                request.Query.PageSize,
                (int)maxPrice,
                (int)minPrice);
        
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