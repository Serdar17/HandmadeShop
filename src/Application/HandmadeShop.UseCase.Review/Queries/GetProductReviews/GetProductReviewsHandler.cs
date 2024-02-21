using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.SharedModel.Reviews.Models;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.UseCase.Review.Queries.GetProductReviews;

internal sealed class GetProductReviewsHandler : IQueryHandler<GetProductReviewQuery, IEnumerable<ReviewInfoModel>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductReviewsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ReviewInfoModel>>> Handle(GetProductReviewQuery request, CancellationToken cancellationToken)
    {
        var productQuery = _context.Products
            .Where(x => x.Uid == request.ProductId);

        if (! await productQuery.AnyAsync(cancellationToken: cancellationToken))
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        var reviews = await productQuery
            .Include(x => x.Reviews)
                .ThenInclude(x => x.Owner)
            .SelectMany(x => x.Reviews)
            .Select(x => _mapper.Map<ReviewInfoModel>(x))
            .ToListAsync(cancellationToken: cancellationToken);

        return reviews;
    }
}