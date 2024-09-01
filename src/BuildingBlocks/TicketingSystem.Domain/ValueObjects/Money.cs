namespace TicketingSystem.Domain.ValueObjects;

public class Money : ValueObject
{
    public required decimal Amount { get; init; }
    public required string Currency { get; init; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}
