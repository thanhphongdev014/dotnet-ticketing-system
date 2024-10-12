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
    public virtual bool? ShouldTrackingEntity { get; protected set; }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate != null ?
            await dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).FirstOrDefaultAsync(predicate, cancellationToken) :
            await dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate != null ?
            await dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).AnyAsync(predicate, cancellationToken) :
            await dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).AnyAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(TKey id)
    {
        var entity = await dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).FirstOrDefaultAsync(x => x.Id!.Equals(id));
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
            var entities = dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).Where(predicate);
            return await entities.ToListAsync(cancellationToken);
        }
        else
        {
            return await dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).ToListAsync(cancellationToken);
        }
    }

    public IQueryable<TEntity> GetQueryableAsync()
    {
        return dbContext.Set<TEntity>().AsNoTrackingIf(!ShouldTracking()).AsQueryable();
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default, bool isAutoSave = false)
    {
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        if (isAutoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default, bool isAutoSave = false)
    {
        if (dbContext.Set<TEntity>().Local.All(e => e != entity))
        {
            dbContext.Set<TEntity>().Attach(entity);
            dbContext.Update(entity);
        }
        if (isAutoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool isAutoSave = false)
    {
        dbContext.Set<TEntity>().Remove(entity);
        if (isAutoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private bool ShouldTracking()
    {
        return ShouldTrackingEntity.HasValue && ShouldTrackingEntity.Value;
    }
}
