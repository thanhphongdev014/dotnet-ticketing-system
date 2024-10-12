using TicketingSystem.Domain.BusinessRules;

namespace TicketingSystem.Services.EventService.Domain.Events.Rules;

public class EventImagesMustInRangeOneToTenRule(IReadOnlyCollection<EventImage> eventImages) : IBusinessRule
{
    public Task<bool> IsBroken()
    {
        return Task.FromResult(eventImages.Count < 1 || eventImages.Count > 10);
    }

    public string Message => "Event images must in range 1 - 10";
}