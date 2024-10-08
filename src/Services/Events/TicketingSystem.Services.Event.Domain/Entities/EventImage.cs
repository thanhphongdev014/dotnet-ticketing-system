using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Services.Event.Domain.Entities;

public class EventImage : Entity<Guid>
{
    public Guid EventId { get; set; }
    public string FileName { get; set; } = string.Empty;
}