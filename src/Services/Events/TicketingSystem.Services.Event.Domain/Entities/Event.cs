using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Services.Event.Domain.Entities;

public class Event
    : AggregateRoot<Guid>, IAuditedObject
{
    public Event(Guid id, string name, DateTimeOffset startDate, DateTimeOffset endDate) : base(id)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Name { get; internal set; }
    public DateTimeOffset StartDate { get; internal set; }
    public DateTimeOffset EndDate { get; internal set; }
    public DateTimeOffset LastModificationTime { get; set; }
    public DateTimeOffset CreationTime { get; set; }
}