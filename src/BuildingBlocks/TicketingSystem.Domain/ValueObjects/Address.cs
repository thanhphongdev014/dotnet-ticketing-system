namespace TicketingSystem.Domain.ValueObjects;

public class Address(string street, string district, string city) : ValueObject
{
    public string Street { get; init; } = street;
    public string District { get; init; } = district;
    public string City { get; init; } = city;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return District;
        yield return City;
    }
}
