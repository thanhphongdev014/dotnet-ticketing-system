using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Shared.ServiceLifetimes;

namespace TicketingSystem.Infrastructure.Extensions;
public static class ServiceCollectionDependencyExtensions 
{
    public static IServiceCollection AddRegisterServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => 
                classes.AssignableTo<ITransientDependency>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.Scan(scan => scan
            .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => 
                classes.AssignableTo<ISingletonDependency>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.Scan(scan => scan
            .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => 
                classes.AssignableTo<IScopedDependency>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
