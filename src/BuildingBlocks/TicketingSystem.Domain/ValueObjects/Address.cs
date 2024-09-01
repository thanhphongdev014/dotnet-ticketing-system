namespace TicketingSystem.Domain.ValueObjects;

public class Address : ValueObject
{
    public required string Street { get; init; }
    public required string District { get; init; }
    public required string City { get; init; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return District;
        yield return City;
    }
}
