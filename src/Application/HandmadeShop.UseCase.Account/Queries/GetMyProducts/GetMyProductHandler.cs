using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Accounts.Models;
using HandmadeShop.SharedModel.Catalogs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.UseCase.Account.Queries.GetMyProducts;

internal sealed class GetMyProductHandler(
    UserManager<User> userManager,
    IMapper mapper,
    IIdentityService identityService)
    : IQueryHandler<GetMyProductQuery, PagedList<ProductModel>>
{
    public async Task<Result<PagedList<ProductModel>>> Handle(GetMyProductQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        
        var userQuery = userManager.Users
            .Include(x => x.Products)
                .ThenInclude(x => x.Catalog)
            .Where(x => x.Id == userId);
        
        if (!await userQuery.AnyAsync(cancellationToken))
        {
            return UserErrors.NotFound(userId);
        }

        IQueryable<Product> productQuery;
        
        if (request.Model.IsFavorite)
        {
            productQuery = userQuery
                .Include(x => x.UserLikes)
                    .ThenInclude(x => x.Like)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Catalog)
                .SelectMany(x => x.UserLikes.Select(u => u.Like.Product));
        }
        else
        {
            productQuery = userQuery
                .SelectMany(x => x.Products);
        }

        var p = productQuery.ToList();

        var productQueryResponse = productQuery
            .Select(x => mapper.Map<ProductModel>(x));

        var products = await PagedList<ProductModel>.CreateAsync(
            productQueryResponse,
            request.Model.Page,
            request.Model.PageSize,
            0,
            0);

        return products;
    }
}