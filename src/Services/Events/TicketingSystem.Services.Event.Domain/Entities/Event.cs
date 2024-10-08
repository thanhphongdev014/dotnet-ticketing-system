using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.ValueObjects;

namespace TicketingSystem.Services.Event.Domain.Entities;

public class Event : AggregateRoot<Guid>, IAuditedObject
{
    internal Event(Guid id, string name, Address address, DateTimeOffset startDate, DateTimeOffset endDate) : base(id)
    {
        Name = name;
        Location = address;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Name { get; internal set; }
    public Address Location { get; set; }
    public DateTimeOffset StartDate { get; internal set; }
    public DateTimeOffset EndDate { get; internal set; }
    public DateTimeOffset LastModificationTime { get; set; }
    public DateTimeOffset CreationTime { get; set; }
}