using Microsoft.EntityFrameworkCore;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Persistence.Repositories;
public static class EfCoreRepositoryExtensions
{
    public static IQueryable<TEntity> AsNoTrackingIf<TEntity>(this IQueryable<TEntity> queryable, bool condition)
        where TEntity : class, IEntity
    {
        return condition ? queryable.AsNoTracking() : queryable;
    }
}
