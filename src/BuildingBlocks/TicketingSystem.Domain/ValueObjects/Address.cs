namespace TicketingSystem.Domain.ValueObjects;

public class Address(string street, string district, string city) : ValueObject
{
    public required string Street { get; init; } = street;
    public required string District { get; init; } = district;
    public required string City { get; init; } = city;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return District;
        yield return City;
    }
}
