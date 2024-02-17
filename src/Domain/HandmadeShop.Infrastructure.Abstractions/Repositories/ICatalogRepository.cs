using HandmadeShop.Domain;

namespace HandmadeShop.Infrastructure.Abstractions.Repositories;

public interface ICatalogRepository : IBaseRepository<Catalog, Guid>
{
    Task<Catalog?> GetByNameAsync(string name);
    Task<bool> IsUniqueAsync(string name);
}