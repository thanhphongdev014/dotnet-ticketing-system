using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Services.Event.Domain.Entities;

public class Event(Guid id, string name, DateTimeOffset startDate, DateTimeOffset endDate)
    : Entity<Guid>(id), IAggregateRoot, IAuditedObject
{
    public string Name { get; set; } = name;
    public DateTimeOffset StartDate { get; set; } = startDate;
    public DateTimeOffset EndDate { get; set; } = endDate;
    public DateTimeOffset LastModificationTime { get; set; }
    public DateTimeOffset CreationTime { get; set; }
}