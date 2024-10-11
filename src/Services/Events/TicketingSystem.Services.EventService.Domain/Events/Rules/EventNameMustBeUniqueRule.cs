using TicketingSystem.Domain.BusinessRules;
using TicketingSystem.Domain.Repositories;

namespace TicketingSystem.Services.EventService.Domain.Events.Rules;

public class EventNameMustBeUniqueRule(IReadOnlyRepository<Event, Guid> repository, string eventName) : IBusinessRule
{
    public async Task<bool> IsBroken()
    {
        return false;
    }

    public string Message => "Event with this name already exists.";
}