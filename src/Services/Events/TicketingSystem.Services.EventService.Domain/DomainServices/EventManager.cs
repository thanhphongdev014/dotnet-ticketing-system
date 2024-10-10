using TicketingSystem.Domain.DomainService;
using TicketingSystem.Domain.ValueObjects;
using TicketingSystem.Services.EventService.Domain.Entities;

namespace TicketingSystem.Services.EventService.Domain.DomainServices;

public class EventManager : DomainService
{
    public async Task CreateAsync(string name, Address address, DateTimeOffset startDate, DateTimeOffset endDate, List<EventImage> images)
    {
        //var test = new Event.Domain
    }
}