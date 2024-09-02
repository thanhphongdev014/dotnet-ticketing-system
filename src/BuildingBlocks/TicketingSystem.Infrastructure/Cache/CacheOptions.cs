namespace TicketingSystem.Infrastructure.Cache;

public class CachingOptions
{
    public InMemoryCacheOptions InMemory { get; set; } = new();

    public DistributedCacheOptions Distributed { get; set; } = new();
}

public class InMemoryCacheOptions
{
    public long? SizeLimit { get; set; }
}

public class DistributedCacheOptions
{
    public string Provider { get; set; } = string.Empty;

    public InMemoryCacheOptions InMemory { get; set; } = new();

    public RedisOptions Redis { get; set; } = new();

    public SqlServerOptions SqlServer { get; set; } = new();
}

public class RedisOptions
{
    public string Configuration { get; set; } = string.Empty;

    public string InstanceName { get; set; } = string.Empty;
}

public class SqlServerOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public string SchemaName { get; set; } = string.Empty;

    public string TableName { get; set; } = string.Empty;
}
