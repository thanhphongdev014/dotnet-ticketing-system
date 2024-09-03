using System.Linq.Expressions;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Domain.Repositories;

public interface IReadOnlyRepository<TEntity, in TKey> where TEntity : Entity<TKey>, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }

    IQueryable<TEntity> GetQueryableAsync();

    Task<TEntity> GetAsync(TKey id);

    Task<List<TEntity>> GetListAsync();

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
}
