using TicketingSystem.Domain.Entities;
using TicketingSystem.Persistence.EntityFrameworkCore;
using TicketingSystem.Persistence.Repositories;

namespace TicketingSystem.Persistence.Decorators;

public class RepositoryDecorator<TEntity, TKey>(BaseDbContext dbContext)
    : EfCoreRepository<TEntity, TKey>(dbContext)
    where TEntity : Entity<TKey>, IAggregateRoot
{
    public override bool? ShouldTrackingEntity { get; protected set; } = true;
}