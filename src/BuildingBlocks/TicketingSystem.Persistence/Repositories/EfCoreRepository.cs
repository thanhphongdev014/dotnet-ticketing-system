using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Exceptions;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Persistence.EntityFrameworkCore;
using TicketingSystem.Persistence.Extensions;

namespace TicketingSystem.Persistence.Repositories;
public class EfCoreRepository<TEntity, TKey>(BaseDbContext dbContext) : IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>, IAggregateRoot
{
    private readonly BaseDbContext _dbContext = dbContext;

    public bool? ShouldTrackingEntity { get; private set; }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate != null ?
            await _dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).FirstOrDefaultAsync(predicate, cancellationToken) :
            await _dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(TKey id)
    {
        var entity = await _dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).FirstOrDefaultAsync(x => x.Id!.Equals(id));
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
            var entities = _dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).Where(predicate);
            return await entities.ToListAsync(cancellationToken);
        }
        else
        {
            return await _dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).ToListAsync(cancellationToken);
        }
    }

    public IQueryable<TEntity> GetQueryableAsync()
    {
        return _dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).AsQueryable();
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default, bool isAutoSave = false)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        if (isAutoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default, bool isAutoSave = false)
    {
        if (_dbContext.Set<TEntity>().Local.All(e => e != entity))
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Update(entity);
        }
        if (isAutoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool isAutoSave = false)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        if (isAutoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private bool ShouldTracking()
    {
        return ShouldTrackingEntity.HasValue && ShouldTrackingEntity.Value;
    }
}
