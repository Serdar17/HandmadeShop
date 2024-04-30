using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;

namespace HandmadeShop.Context;

public class UnitOfWork(
    IAppDbContext context,
    Lazy<ICatalogRepository> catalogRepository,
    Lazy<IProductRepository> productRepository,
    Lazy<ILikeRepository> likeRepository,
    Lazy<IReviewRepository> reviewRepository,
    Lazy<IOrderRepository> orderRepository,
    Lazy<IBuyerRepository> buyerRepository)
    : IUnitOfWork
{
    public ICatalogRepository CatalogRepository => catalogRepository.Value;
    public IProductRepository ProductRepository => productRepository.Value;
    public ILikeRepository LikeRepository => likeRepository.Value;
    public IReviewRepository ReviewRepository => reviewRepository.Value;
    public IOrderRepository OrderRepository => orderRepository.Value;
    public IBuyerRepository BuyerRepository => buyerRepository.Value;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        context.Dispose();
    }
}