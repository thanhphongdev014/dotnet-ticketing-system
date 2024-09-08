namespace TicketingSystem.Infrastructure.MessageBrokers.RabbitMQ;

public class RabbitMqOptions
{
    public string HostName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ExchangeName { get; set; } = string.Empty;

    public Dictionary<string, string> RoutingKeys { get; set; } = new();

    public Dictionary<string, Dictionary<string, string>> Consumers { get; set; } = new();

    public string ConnectionString => $"amqp://{UserName}:{Password}@{HostName}/%2f";

    public bool MessageEncryptionEnabled { get; set; }

    public string MessageEncryptionKey { get; set; } = string.Empty;
}
