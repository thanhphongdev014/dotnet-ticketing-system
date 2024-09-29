using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TicketingSystem.Domain;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Persistence;
using TicketingSystem.Persistence.Repositories;

namespace TicketingSystem.Infrastructure.Extensions;
public static class ServiceCollectionRepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddDefaultRepositories()
                .AddCustomRepositories();
        return services;
    }

    private static IServiceCollection AddDefaultRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<,>), typeof(EfCoreRepository<,>));
        services.AddTransient(typeof(IReadOnlyRepository<,>), typeof(EfCoreRepository<,>));
        return services;
    }

    private static void AddCustomRepositories(this IServiceCollection services)
    {
        services.Scan(selector => selector
            .FromAssemblies(
                typeof(DomainAssembly).Assembly,
                typeof(PersistenceAssembly).Assembly)
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
    }

}
