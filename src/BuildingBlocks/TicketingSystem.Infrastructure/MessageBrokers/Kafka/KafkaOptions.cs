namespace TicketingSystem.Infrastructure.MessageBrokers.Kafka;

public class KafkaOptions
{
    public string BootstrapServers { get; set; } = string.Empty;

    public string GroupId { get; set; } = string.Empty;

    public Dictionary<string, string> Topics { get; set; } = new();
}
