using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics.CodeAnalysis;

namespace TicketingSystem.Infrastructure.Cache;

public interface IDistributedCache<TCacheItem>
{
    Task<TCacheItem?> GetAsync(
        string key,
        CancellationToken token = default
    );

    Task<TCacheItem?[]> GetManyAsync(
        IEnumerable<string> keys,
        CancellationToken token = default
    );

    Task SetAsync(
        [NotNull] string key,
        [NotNull] TCacheItem value,
        DistributedCacheEntryOptions? options = null,
        CancellationToken token = default
    );

    Task SetManyAsync(
        IEnumerable<KeyValuePair<string, TCacheItem>> items,
        DistributedCacheEntryOptions? options = null,
        CancellationToken token = default
    );

    Task RemoveAsync(
        string key,
        CancellationToken token = default
    );

    Task RemoveManyAsync(
        IEnumerable<string> keys,
        CancellationToken token = default
    );
}
