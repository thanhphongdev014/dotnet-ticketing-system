using TicketingSystem.Infrastructure.MessageBrokers.Kafka;
using TicketingSystem.Infrastructure.MessageBrokers.RabbitMQ;
using TicketingSystem.Infrastructure.Shared;

namespace TicketingSystem.Infrastructure.MessageBrokers;

public class MessageBrokerOptions
{
    public MessageBrokerEnum Provider { get; set; }

    public RabbitMqOptions RabbitMq { get; set; } = new();

    public KafkaOptions Kafka { get; set; } = new();
}
