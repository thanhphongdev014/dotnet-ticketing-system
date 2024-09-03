namespace TicketingSystem.Domain.ValueObjects;

public class Money(decimal amount, string currency) : ValueObject
{
    public required decimal Amount { get; init; } = amount;
    public required string Currency { get; init; } = currency;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}
