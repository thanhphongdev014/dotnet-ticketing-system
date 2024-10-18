using MediatR;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Domain.ValueObjects;
using TicketingSystem.Services.EventService.Domain.Events;
using TicketSystem.Application.Identity;

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

public class AddEventCommandHandler(
    IRepository<Event, Guid> eventRepository,
    EventManager eventManager,
    ICurrentUser currentUser) : IRequestHandler<AddEventCommand>
{
    public async Task Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUser.UserId ?? Guid.Empty; // change later
        var address = new Address(request.Street, request.District, request.City)
        {
            Street = request.Street,
            District = request.District,
            City = request.City
        };
        var eventId = Guid.NewGuid();
        var eventEntity = await eventManager.CreateAsync(eventId, request.Name, currentUserId, address,
            request.StartDate, request.EndDate,
            request.ImageNames.Select(x => new EventImage(Guid.NewGuid(), eventId, x)).ToList());
    }
}