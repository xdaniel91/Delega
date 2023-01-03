using Microsoft.Extensions.Caching.Distributed;

namespace Delega.Api.Configurations;

public static class AddRedisConfig
{
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("delega.redis");
        });
    }

    public static DistributedCacheEntryOptions Options = new DistributedCacheEntryOptions()
        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
        .SetSlidingExpiration(TimeSpan.FromMinutes(3));
}
