using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TicketingSystem.Domain.Events;
using TicketingSystem.Infrastructure.OutboxMessages;
using TicketingSystem.Persistence.EntityFrameworkCore;

namespace TicketingSystem.Persistence.OutboxMessages;

public class ProcessOutboxJob(BaseDbContext dbContext, IMediator mediator) : IProcessOutboxJob
{
    private const int BatchSize = 20;

    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public async Task ProcessAsync()
    {
        var messages = await GetOutboxMessagesAsync();
        foreach (var message in messages)
        {
            using var transactionScope = new TransactionScope();

            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Data);
            await mediator.Publish(domainEvent);

            message.ProcessedDate = DateTime.Now;
            dbContext.OutboxMessages.Update(message);
            await dbContext.SaveChangesAsync();

            transactionScope.Complete();
        }
    }

    private async Task<IReadOnlyCollection<OutboxMessage>> GetOutboxMessagesAsync()
    {
        var messages = dbContext.OutboxMessages.Where(x => !x.ProcessedDate.HasValue).Take(BatchSize);
        return await messages.ToListAsync();
    }
}