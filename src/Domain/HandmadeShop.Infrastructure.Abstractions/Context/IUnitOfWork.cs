﻿using HandmadeShop.Infrastructure.Abstractions.Repositories;

namespace HandmadeShop.Infrastructure.Abstractions.Context;

public interface IUnitOfWork : IDisposable
{
    public ICatalogRepository CatalogRepository { get; }
    public IProductRepository ProductRepository { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}