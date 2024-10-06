using TicketingSystem.Domain.Events;

namespace TicketingSystem.Infrastructure.OutboxMessages;

public class OutboxMessage
{
    public Guid Id { get; set; }

    public DateTimeOffset OccurredOn { get; set; }

    public string Type { get; set; }

    public EventType EventType { get; set; }

    public string Data { get; set; }

    public DateTime? ProcessedDate { get; set; }

    private OutboxMessage()
    {
    }

    public OutboxMessage(DateTimeOffset occurredOn, string type, EventType eventType, string data)
    {
        Id = Guid.NewGuid();
        OccurredOn = occurredOn;
        Type = type;
        EventType = eventType;
        Data = data;
    }
}