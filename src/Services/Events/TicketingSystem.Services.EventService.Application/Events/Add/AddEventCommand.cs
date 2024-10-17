using MediatR;

namespace TicketingSystem.Services.EventService.Application.Events.Add;

public class AddEventCommand(
    string name,
    string street,
    string district,
    string city,
    DateTimeOffset startDate,
    DateTimeOffset endDate,
    List<string> imageNames) : IRequest
{
    public string Name { get; set; } = name;
    public string Street { get; set; } = street;
    public string District { get; set; } = district;
    public string City { get; set; } = city;
    public DateTimeOffset StartDate { get; set; } = startDate;
    public DateTimeOffset EndDate { get; set; } = endDate;
    public List<string> ImageNames { get; set; } = imageNames;
}

public class AddEventCommandHandler : IRequestHandler<AddEventCommand>
{
    public Task Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}