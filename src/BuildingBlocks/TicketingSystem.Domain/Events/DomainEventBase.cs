namespace TicketingSystem.Domain.Events;

public class DomainEventBase : IDomainEvent
{
    public DomainEventBase()
    {
        this.OccurredOn = DateTime.Now;
    }

    public DateTimeOffset OccurredOn { get; }
}
