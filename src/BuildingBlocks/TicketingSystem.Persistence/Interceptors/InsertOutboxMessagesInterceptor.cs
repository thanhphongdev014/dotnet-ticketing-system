using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Events;
using TicketingSystem.Infrastructure.OutboxMessage;

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
        context
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
            .Select(domainEvent => new OutboxMessage
            {
                Id = domainEvent.Id,
                OccurredOnUtc = domainEvent.OccurredOnUtc,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(domainEvent, Serializer)
            })
            .ToList();

        context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}