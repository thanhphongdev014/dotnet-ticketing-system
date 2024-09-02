using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace TicketingSystem.Infrastructure.Cache;

public class RedisDistributedCache<TCacheItem>(IDistributedCache distributedCache)
    : IDistributedCache<TCacheItem> where TCacheItem : class
{
    private DistributedCacheEntryOptions _defaultOption = new();

    public async Task<TCacheItem?> GetAsync(string key, CancellationToken token = default)
    {
        var bytes = await distributedCache.GetAsync(key, token);
        return ToCacheItem(bytes);
    }

    public async Task<TCacheItem?[]> GetManyAsync(IEnumerable<string> keys, CancellationToken token = default)
    {
        var result = new List<TCacheItem>();
        foreach (var key in keys)
        {
            var bytes = await distributedCache.GetAsync(key, token);
            var cacheItem = ToCacheItem(bytes);
            if (cacheItem != null)
            {
                result.Add(cacheItem);
            }
        }
        return result.ToArray();
    }

    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        await distributedCache.RemoveAsync(key, token);
    }

    public async Task RemoveManyAsync(IEnumerable<string> keys, CancellationToken token = default)
    {
        foreach (var key in keys)
        {
            await distributedCache.RemoveAsync(key, token);
        }
    }

    public async Task SetAsync([NotNull] string key, [NotNull] TCacheItem value, DistributedCacheEntryOptions? options = null, CancellationToken token = default)
    {
        var json = JsonSerializer.Serialize(value, CreateJsonSerializerOptions());
        await distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(json), options ?? _defaultOption, token);
    }

    public async Task SetManyAsync(IEnumerable<KeyValuePair<string, TCacheItem>> items, DistributedCacheEntryOptions? options = null, CancellationToken token = default)
    {
        foreach (var item in items)
        {
            await SetAsync(item.Key, item.Value, options, token);
        }
    }

    private TCacheItem? ToCacheItem(byte[]? bytes)
    {
        if (bytes == null)
        {
            return null;
        }

        return (TCacheItem?)JsonSerializer.Deserialize(Encoding.UTF8.GetString(bytes), typeof(TCacheItem), CreateJsonSerializerOptions());
    }

    private static readonly ConcurrentDictionary<object, JsonSerializerOptions> JsonSerializerOptionsCache =
        new ConcurrentDictionary<object, JsonSerializerOptions>();

    protected virtual JsonSerializerOptions CreateJsonSerializerOptions(bool camelCase = true, bool indented = false)
    {
        var option = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };
        return JsonSerializerOptionsCache.GetOrAdd(new
        {
            camelCase,
            indented,
            option
        }, _ =>
        {
            var settings = new JsonSerializerOptions(option);

            if (camelCase)
            {
                settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }

            if (indented)
            {
                settings.WriteIndented = true;
            }

            return settings;
        });
    }
}
