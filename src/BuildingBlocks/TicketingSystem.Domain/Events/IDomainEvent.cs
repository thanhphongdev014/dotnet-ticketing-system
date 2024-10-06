using MediatR;

namespace TicketingSystem.Domain.Events;

public interface IDomainEvent : INotification
{
    public DateTimeOffset OccurredOn { get; }
    EventType EventType { get; set; }
}
