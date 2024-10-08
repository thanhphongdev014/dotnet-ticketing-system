using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.ValueObjects;
using TicketingSystem.Services.Event.Domain.Enums;

namespace TicketingSystem.Services.Event.Domain.Entities;

public class Ticket(Guid id, string seat, Money price, TicketStatus status)
    : AggregateRoot<Guid>(id), IAuditedObject
{
    public Guid EventId { get; internal set; }
    public string Seat { get; internal set; } = seat;
    public Money Price { get; internal set; } = price;
    public TicketStatus TicketStatus { get; internal set; } = status;
    public DateTimeOffset LastModificationTime { get; set; }
    public DateTimeOffset CreationTime { get; set; }
}