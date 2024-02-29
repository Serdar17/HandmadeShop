using HandmadeShop.Domain;

namespace HandmadeShop.Infrastructure.Abstractions.Repositories;

public interface IOrderRepository : IBaseRepository<Order, Guid>
{
}