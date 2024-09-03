using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;


namespace TicketingSystem.Infrastructure.Hangfire;

public class HangfireAuthorizationFilter(string policyName)
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var authService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
        return authService.AuthorizeAsync(httpContext.User, policyName).ConfigureAwait(false).GetAwaiter().GetResult().Succeeded;
    }
}
