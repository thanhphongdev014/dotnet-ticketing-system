using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Persistence.EntityFrameworkCore;
public class BaseDbContext : DbContext
{
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
