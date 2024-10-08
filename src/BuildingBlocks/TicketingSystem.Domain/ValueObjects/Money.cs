namespace TicketingSystem.Domain.ValueObjects;

public class Money(decimal amount, string currency) : ValueObject
{
    public required decimal Price { get; init; } = amount;
    public required string Currency { get; init; } = currency;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Price;
        yield return Currency;
    }
}
