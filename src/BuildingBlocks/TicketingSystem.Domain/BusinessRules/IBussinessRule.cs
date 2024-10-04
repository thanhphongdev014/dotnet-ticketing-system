namespace TicketingSystem.Domain.BusinessRules;

public interface IBusinessRule
{
    Task<bool> IsBroken();

    string Message { get; }
}