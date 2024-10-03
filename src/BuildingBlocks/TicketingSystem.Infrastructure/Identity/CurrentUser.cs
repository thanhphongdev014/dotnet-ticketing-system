using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TicketSystem.Application.Identity;

namespace TicketingSystem.Infrastructure.Identity;

public class CurrentWebUser(IHttpContextAccessor context) : ICurrentUser
{
    public bool IsAuthenticated => context.HttpContext?.User.Identity != null && context.HttpContext != null &&
                                   context.HttpContext.User.Identity.IsAuthenticated;

    public Guid? UserId
    {
        get
        {
            var userId = context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? context.HttpContext?.User.FindFirst("sub")?.Value;

            if (userId != null)
            {
                return Guid.Parse(userId);
            }

            return null;
        }
    }
}