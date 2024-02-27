using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;

namespace HandmadeShop.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAppDbContext _context;
    private readonly Lazy<ICatalogRepository> _catalogRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly Lazy<ILikeRepository> _likeRepository;
    private readonly Lazy<IReviewRepository> _reviewRepository;
    private readonly Lazy<IOrderRepository> _orderRepository;
    private readonly Lazy<IBuyerRepository> _buyerRepository;
    
    public UnitOfWork(
        IAppDbContext context, 
        Lazy<ICatalogRepository> catalogRepository, 
        Lazy<IProductRepository> productRepository, 
        Lazy<ILikeRepository> likeRepository, 
        Lazy<IReviewRepository> reviewRepository, 
        Lazy<IOrderRepository> orderRepository, 
        Lazy<IBuyerRepository> buyerRepository)
    {
        _context = context;
        _catalogRepository = catalogRepository;
        _productRepository = productRepository;
        _likeRepository = likeRepository;
        _reviewRepository = reviewRepository;
        _orderRepository = orderRepository;
        _buyerRepository = buyerRepository;
    }

    public ICatalogRepository CatalogRepository => _catalogRepository.Value;
    public IProductRepository ProductRepository => _productRepository.Value;
    public ILikeRepository LikeRepository => _likeRepository.Value;
    public IReviewRepository ReviewRepository => _reviewRepository.Value;
    public IOrderRepository OrderRepository => _orderRepository.Value;
    public IBuyerRepository BuyerRepository => _buyerRepository.Value;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}