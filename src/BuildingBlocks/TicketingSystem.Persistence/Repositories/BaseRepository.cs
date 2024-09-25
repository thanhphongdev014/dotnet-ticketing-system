using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Exceptions;
using TicketingSystem.Persistence.EntityFrameworkCore;

namespace TicketingSystem.Persistence.Repositories;
public class BaseRepository<TEntity, TKey>(BaseDbContext dbContext) where TEntity : Entity<TKey>, IAggregateRoot
{
    private readonly BaseDbContext _dbContext = dbContext;

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate != null ?
            await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken) :
            await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(TKey id)
    {
        var entity = await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id!.Equals(id));
        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }
        return entity;
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
