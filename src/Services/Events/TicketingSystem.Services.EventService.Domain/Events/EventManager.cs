using TicketingSystem.Domain.BusinessRules.CommonRules;
using TicketingSystem.Domain.DomainService;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Domain.ValueObjects;
using TicketingSystem.Services.EventService.Domain.Events.Rules;

namespace TicketingSystem.Services.EventService.Domain.Events;

public class EventManager(IReadOnlyRepository<Event, Guid> repository) : DomainService
{
    public async Task<Event> CreateAsync(string name, Guid userId, Address address, DateTimeOffset startDate,
        DateTimeOffset endDate, List<EventImage> images)
    {
        await CheckRule(new NameMustBeUniqueRule<Event, Guid>(repository, name));
        await CheckRule(new StartDateCannotLessThanCurrentDateRule(startDate));
        await CheckRule(new EventImagesMustInRangeOneToTenRule(images));
        return new Event(Guid.NewGuid(), name, userId, address, startDate, endDate, images);
    }

    public async Task ChangeNameAsync(Event eventEntity, string name)
    {
        await CheckRule(new NameMustBeUniqueRule<Event, Guid>(repository, name));
        eventEntity.Name = name;
    }

    public async Task ChangeStartDateAsync(Event eventEntity, DateTimeOffset startDate)
    {
        await CheckRule(new StartDateCannotLessThanCurrentDateRule(startDate));
        eventEntity.StartDate = startDate;
    }

    public Task CheckEventTicketIsSold(Guid eventId)
    {
        // implement later
        return Task.CompletedTask;
    }
}