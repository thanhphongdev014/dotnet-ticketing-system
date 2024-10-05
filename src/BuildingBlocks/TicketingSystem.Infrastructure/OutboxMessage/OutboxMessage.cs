using TicketingSystem.Domain.Events;

namespace TicketingSystem.Infrastructure.OutboxMessage;

public class OutboxMessage
{
    public Guid Id { get; set; }

    public DateTime OccurredOn { get; set; }

    public string Type { get; set; } = null!;

    public EventType EventType { get; set; }

    public string Data { get; set; } = null!;

    public DateTime? ProcessedDate { get; set; }

    private OutboxMessage()
    {
            
    }

    public OutboxMessage(DateTime occurredOn, string type, string data)
    {
        Id = Guid.NewGuid();
        OccurredOn = occurredOn;
        Type = type;
        Data = data;
    }
}