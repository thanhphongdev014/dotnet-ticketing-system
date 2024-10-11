using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.ValueObjects;

namespace TicketingSystem.Services.EventService.Domain.Events;

public class Event : AggregateRoot<Guid>, IAuditedObject
{
    internal Event(Guid id, string name, Address address, DateTimeOffset startDate, DateTimeOffset endDate, List<EventImage> images) : base(id)
    {
        Name = name;
        Location = address;
        StartDate = startDate;
        EndDate = endDate;
        Images = images;
    }

    public string Name { get; internal set; }
    public Address Location { get; set; }
    public DateTimeOffset StartDate { get; internal set; }
    public DateTimeOffset EndDate { get; internal set; }
    public DateTimeOffset LastModificationTime { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public List<EventImage> Images { get; private set; }
}