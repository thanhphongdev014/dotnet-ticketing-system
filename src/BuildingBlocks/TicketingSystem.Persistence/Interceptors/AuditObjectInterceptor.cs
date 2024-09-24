using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Persistence.Interceptors;
public class AuditObjectInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is { State: EntityState.Added, Entity: IAuditedObject addedObject })
            {
                addedObject.CreationTime = DateTimeOffset.Now;
            }
            if (entry is { State: EntityState.Modified, Entity: IAuditedObject modifiedObject })
            {
                modifiedObject.LastModificationTime = DateTimeOffset.Now;
            }
        }
        return result;
    }
}
