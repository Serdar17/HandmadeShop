using HandmadeShop.Context.Repositories;
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
    
    public UnitOfWork(
        IAppDbContext context, 
        Lazy<ICatalogRepository> catalogRepository, 
        Lazy<IProductRepository> productRepository, 
        Lazy<ILikeRepository> likeRepository, 
        Lazy<IReviewRepository> reviewRepository)
    {
        _context = context;
        _catalogRepository = catalogRepository;
        _productRepository = productRepository;
        _likeRepository = likeRepository;
        _reviewRepository = reviewRepository;
    }

    public ICatalogRepository CatalogRepository => _catalogRepository.Value;
    public IProductRepository ProductRepository => _productRepository.Value;
    public ILikeRepository LikeRepository => _likeRepository.Value;
    public IReviewRepository ReviewRepository => _reviewRepository.Value;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}