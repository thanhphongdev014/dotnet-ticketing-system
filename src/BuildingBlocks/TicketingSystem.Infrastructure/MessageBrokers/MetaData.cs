namespace TicketingSystem.Infrastructure.MessageBrokers;

public class MetaData
{
    public string MessageId { get; set; } = string.Empty;

    public string MessageVersion { get; set; } = string.Empty;

    public string CorrelationId { get; set; } = string.Empty;

    public DateTimeOffset? CreationDateTime { get; set; }

    public DateTimeOffset? EnqueuedDateTime { get; set; }

    public string AccessToken { get; set; } = string.Empty;
}
