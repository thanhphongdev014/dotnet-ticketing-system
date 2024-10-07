using TicketingSystem.Domain.BusinessRules;

namespace TicketingSystem.Domain.DomainService;

public class DomainService : IDomainService
{
    protected static async Task CheckRule(IBusinessRule rule)
    {
        if (await rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}