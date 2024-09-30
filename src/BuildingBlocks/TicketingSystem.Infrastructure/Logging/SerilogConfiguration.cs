using System.Net;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace TicketingSystem.Infrastructure.Logging;

public static class SerilogConfiguration
{
    public static IHostBuilder UseTicketingSystemSerilog(this IHostBuilder hostBuilder)
    {
        var hostName = Dns.GetHostName();
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
#else
             .MinimumLevel.Warning()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
#endif
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.WithProperty("ServerName", hostName)
            .Enrich.WithProperty("Environment",
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production")
            .Enrich.FromLogContext()
#if DEBUG
            .WriteTo.Async(c => c.File($"Logs/logs-.log", rollingInterval: RollingInterval.Day))
            .WriteTo.Async(c => c.Console())
#else
            .WriteTo.Async(c => c.File(new Elastic.CommonSchema.Serilog.EcsTextFormatter(), $"Logs/{hostName}_logs-.json", rollingInterval: RollingInterval.Day))
            .WriteTo.Async(c => c.Console())
#endif
            .CreateLogger();
        return hostBuilder.UseSerilog();
    }
}