using System.Linq.Expressions;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Domain.Repositories;

public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }

    IQueryable<TEntity> GetQueryableAsync();

    Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity);

    Task<TEntity> GetAsync(TKey id);

    Task<List<TEntity>> GetListAsync();

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
}
