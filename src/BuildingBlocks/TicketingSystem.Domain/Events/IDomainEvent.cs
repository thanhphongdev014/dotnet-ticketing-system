using MediatR;

namespace TicketingSystem.Domain.Events;

public interface IDomainEvent : INotification
{
    EventType EventType { get; set; }
}
