namespace TicketingSystem.Infrastructure.Hangfire;

public interface IHangfireBackgroundWorker
{
    string? RecurringJobId { get; set; }

    string CronExpression { get; set; }

    TimeZoneInfo? TimeZone { get; set; }

    string Queue { get; set; }

    Task DoWorkAsync(CancellationToken cancellationToken = default);
}
