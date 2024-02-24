namespace HandmadeShop.Infrastructure.Abstractions.Caching;

public interface ICacheService
{
    /// <summary>
    /// Get T object from cache
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="resetLifeTime">Time for data store</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <typeparam name="T">Generic data</typeparam>
    Task<T?> GetAsync<T>(string key, bool resetLifeTime = false, CancellationToken cancellationToken = default)
        where T : class;
    
    /// <summary>
    /// Put data to cache
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="data">Data</param>
    /// <param name="storeTime">Time for data store</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <typeparam name="T">Generic data object</typeparam>
    Task PutAsync<T>(string key, T data, TimeSpan? storeTime = null, CancellationToken cancellationToken = default)
        where T : class?;
    
    /// <summary>
    /// Remove value from cache
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Set new store time
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="storeTime">New time for data store</param>
    Task SetStoreTimeAsync(string key, TimeSpan? storeTime = null);
    
}