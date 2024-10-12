using TicketingSystem.Domain.BusinessRules;

namespace TicketingSystem.Services.EventService.Domain.Events.Rules;

public class StartDateCannotLessThanCurrentDateRule(DateTimeOffset startDate) : IBusinessRule
{
    public Task<bool> IsBroken()
    {
        return Task.FromResult(startDate.Date < DateTime.Today);
    }

    public string Message => "Start date must less than current date.";
}