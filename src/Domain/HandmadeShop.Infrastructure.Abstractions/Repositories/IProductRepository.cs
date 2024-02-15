using HandmadeShop.Domain;

namespace HandmadeShop.Infrastructure.Abstractions.Repositories;

public interface IProductRepository : IBaseRepository<Product, Guid>
{
    
}