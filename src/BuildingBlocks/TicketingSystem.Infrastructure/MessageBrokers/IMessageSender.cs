namespace TicketingSystem.Infrastructure.MessageBrokers;

public interface IMessageSender<in T>
{
    Task SendAsync(T message, MetaData metaData, CancellationToken cancellationToken = default);
}
