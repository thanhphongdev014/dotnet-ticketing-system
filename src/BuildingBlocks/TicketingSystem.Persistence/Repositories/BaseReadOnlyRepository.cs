using System.Linq.Expressions;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Persistence.EntityFrameworkCore;

namespace TicketingSystem.Persistence.Repositories;
public class BaseReadOnlyRepository<TEntity, TKey>(BaseDbContext dbContext) : IReadOnlyRepository<TEntity, TKey>
    where TEntity : Entity<TKey>, IAggregateRoot
{
    private readonly BaseDbContext _dbContext = dbContext;

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> GetQueryableAsync()
    {
        throw new NotImplementedException();
    }
}
