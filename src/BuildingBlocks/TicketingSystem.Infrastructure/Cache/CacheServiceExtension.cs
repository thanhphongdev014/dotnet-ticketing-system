using Microsoft.Extensions.DependencyInjection;

namespace TicketingSystem.Infrastructure.Cache;
public static class CacheServiceExtension
{
    public static IServiceCollection AddCaches(this IServiceCollection services, CachingOptions? options = null)
    {
        services.AddMemoryCache(opt =>
        {
            opt.SizeLimit = options?.InMemory?.SizeLimit;
        });

        var distributedProvider = options?.Distributed?.Provider;

        if (distributedProvider == "InMemory")
        {
            services.AddDistributedMemoryCache(opt =>
            {
                opt.SizeLimit = options?.Distributed?.InMemory?.SizeLimit;
            });
        }
        else if (distributedProvider == "Redis")
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = options?.Distributed.Redis.Configuration;
                opt.InstanceName = options?.Distributed.Redis.InstanceName;
            });
        }

        return services;
    }
}
