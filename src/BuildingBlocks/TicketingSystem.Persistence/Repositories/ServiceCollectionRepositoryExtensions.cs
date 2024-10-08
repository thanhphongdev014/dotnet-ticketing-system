﻿using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TicketingSystem.Domain;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Persistence.Decorators;

namespace TicketingSystem.Persistence.Repositories;

public static class ServiceCollectionRepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddDefaultRepositories()
            .AddCustomRepositories()
            .Decorate(typeof(IRepository<,>), typeof(RepositoryDecorator<,>));
        return services;
    }

    private static IServiceCollection AddDefaultRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<,>), typeof(EfCoreRepository<,>));
        services.AddTransient(typeof(IReadOnlyRepository<,>), typeof(EfCoreRepository<,>));
        return services;
    }

    private static IServiceCollection AddCustomRepositories(this IServiceCollection services)
    {
        return services.Scan(selector => selector
            .FromAssemblies(
                typeof(DomainAssembly).Assembly,
                typeof(PersistenceAssembly).Assembly)
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
    }
}