using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Events;
using TicketingSystem.Infrastructure.OutboxMessages;

namespace TicketingSystem.Persistence.Interceptors;

public sealed class InsertOutboxMessagesInterceptor : SaveChangesInterceptor
{
    private static readonly JsonSerializerSettings Serializer = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            InsertOutboxMessages(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsertOutboxMessages(DbContext context)
    {
        var outboxMessages = context
            .ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = new List<IDomainEvent>();
                var localDomainEvents = entity.LocalEvents;
                var distributedDomainEvents = entity.DistributedEvents;

                entity.ClearLocalEvents();
                entity.ClearDistributedEvents();

                domainEvents.AddRange(localDomainEvents);
                domainEvents.AddRange(distributedDomainEvents);

                return domainEvents;
            })
            .Select(domainEvent =>
            {
                var type = domainEvent.GetType().FullName ?? string.Empty;
                var data = JsonConvert.SerializeObject(domainEvent, Serializer);
                
                return new OutboxMessage(domainEvent.OccurredOn,
                    type,
                    domainEvent.EventType,
                    data
                );
            })
            .ToList();

        context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}