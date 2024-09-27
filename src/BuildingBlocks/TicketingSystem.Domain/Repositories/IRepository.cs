using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Domain.Repositories;

public interface IRepository<TEntity, in TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : Entity<TKey>, IAggregateRoot
{
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default, bool isAutoSave = false);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default, bool isAutoSave = false);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default, bool isAutoSave = false);
}
