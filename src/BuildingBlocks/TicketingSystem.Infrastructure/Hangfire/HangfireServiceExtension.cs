using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Reflection;
using TicketingSystem.Infrastructure.Shared;

namespace TicketingSystem.Infrastructure.Hangfire;

public static class HangfireServiceExtension
{
    public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration,
        DatabaseEnum storageProvider, Assembly workersAssembly)
    {

        switch (storageProvider)
        {
            case DatabaseEnum.SqlServer:
                {
                    CreatePostgresDbIfNotExist(configuration);
                    services.AddHangfire(config =>
                        config.UsePostgreSqlStorage(c =>
                        c.UseNpgsqlConnection(configuration.GetConnectionString("Hangfire"))));
                    break;
                }
            case DatabaseEnum.PostgresSql:
                {
                    // create sql server db later
                    services.AddHangfire(config =>
                        config.UseSqlServerStorage(configuration.GetConnectionString("Hangfire")));
                    break;
                }
        }
        services.AddHangfireServer();

        var types = workersAssembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.GetInterface(nameof(IHangfireBackgroundWorker)) != null);

        foreach (var type in types)
        {
            services.AddSingleton(type);
        }

        return services;
    }

    public static void UseHangfireWorkers(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard();
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var workers = scope.ServiceProvider.GetServices<IHangfireBackgroundWorker>();
        foreach (var worker in workers)
        {
            RecurringJob.AddOrUpdate(worker.GetType().Name,
                () => worker.DoWorkAsync(CancellationToken.None), worker.CronExpression, new RecurringJobOptions()
                {
                    TimeZone = worker.TimeZone
                });
        }
    }

    private static void CreatePostgresDbIfNotExist(IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("Hangfire")!;

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connString);
        var databaseName = connectionStringBuilder.Database;
        connectionStringBuilder.Database = "postgres";

        using var connection = new NpgsqlConnection(connectionStringBuilder.ToString());
        connection.Open();

        using var checkCommand = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname='{databaseName}'", connection);
        var exists = (int?)checkCommand.ExecuteScalar() == 1;

        if (!exists)
        {
            using var command = new NpgsqlCommand($"CREATE DATABASE \"{databaseName}\";", connection);
            command.ExecuteNonQuery();
        }
    }
}
