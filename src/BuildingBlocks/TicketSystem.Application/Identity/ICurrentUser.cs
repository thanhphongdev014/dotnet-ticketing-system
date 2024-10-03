namespace TicketSystem.Application.Identity;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }

    Guid? UserId { get; }
}