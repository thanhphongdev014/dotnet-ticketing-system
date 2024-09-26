using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Exceptions;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Persistence.EntityFrameworkCore;

namespace TicketingSystem.Persistence.Repositories;
public class EfCoreRepository<TEntity, TKey>(BaseDbContext dbContext) : IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>, IAggregateRoot
{
    private readonly BaseDbContext _dbContext = dbContext;

    public bool? ShouldTrackingEntity { get; protected set; }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

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

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate != null)
        {
            var entities = _dbContext.Set<TEntity>().AsNoTracking().Where(predicate);
            return await entities.ToListAsync(cancellationToken);
        }
        else
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        }
    }


    public IQueryable<TEntity> GetQueryableAsync()
    {
        return _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
