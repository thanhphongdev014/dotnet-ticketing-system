using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Services.EventService.Domain.Entities;

public class EventImage(Guid id, Guid eventId, string fileName) : Entity<Guid>(id)
{
    public Guid EventId { get; set; } = eventId;
    public string FileName { get; set; } = fileName;
}