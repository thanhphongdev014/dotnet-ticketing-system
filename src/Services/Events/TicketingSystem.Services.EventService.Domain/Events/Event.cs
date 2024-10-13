using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.ValueObjects;

namespace TicketingSystem.Services.EventService.Domain.Events;

public class Event : AggregateRoot<Guid>, IAuditedObject, IHasNameEntity
{
    internal Event(Guid id, string name, Guid userId, Address address, DateTimeOffset startDate, DateTimeOffset endDate, List<EventImage> images) : base(id)
    {
        Name = name;
        UserId = userId;
        Location = address;
        StartDate = startDate;
        EndDate = endDate;
        Images = images;
    }

    public string Name { get; internal set; }
    public Guid UserId { get; set; }
    public Address Location { get; set; }
    public DateTimeOffset StartDate { get; internal set; }
    public DateTimeOffset EndDate { get; internal set; }
    public DateTimeOffset LastModificationTime { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public List<EventImage> Images { get; internal set; }
}