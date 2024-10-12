using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Repositories;

namespace TicketingSystem.Domain.BusinessRules.CommonRules;

public class NameMustBeUniqueRule<TEntity, TKey>(IReadOnlyRepository<TEntity, TKey> repository, string name)
    : IBusinessRule
    where TEntity : Entity<TKey>, IAggregateRoot
{
    public async Task<bool> IsBroken()
    {
        return await repository.AnyAsync(x => ((dynamic)x).Name == name);
    }

    public string Message => "Event with this name already exists.";
}