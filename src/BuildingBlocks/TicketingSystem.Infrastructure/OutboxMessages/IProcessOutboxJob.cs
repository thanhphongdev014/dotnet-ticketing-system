namespace TicketingSystem.Infrastructure.OutboxMessages;

public interface IProcessOutboxJob
{
    Task ProcessAsync();
}