﻿namespace TicketingSystem.Domain.Events;

public class DomainEventBase : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTime.Now;
    public EventType EventType { get; set; }
}
