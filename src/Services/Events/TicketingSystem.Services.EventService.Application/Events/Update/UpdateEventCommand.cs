using MediatR;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Domain.ValueObjects;
using TicketingSystem.Services.EventService.Domain.Events;

namespace TicketingSystem.Services.EventService.Application.Events.Update;

public class UpdateEventCommand(
    Guid eventId,
    string name,
    string street,
    string district,
    string city,
    DateTimeOffset startDate,
    DateTimeOffset endDate,
    List<string> imageNames) : IRequest
{
    public Guid EventId { get; set; } = eventId;
    public string Name { get; set; } = name;
    public string Street { get; set; } = street;
    public string District { get; set; } = district;
    public string City { get; set; } = city;
    public DateTimeOffset StartDate { get; set; } = startDate;
    public DateTimeOffset EndDate { get; set; } = endDate;
    public List<string> ImageNames { get; set; } = imageNames;
}

public class UpdateEventCommandHandler(
    IRepository<Event, Guid> eventRepository,
    EventManager eventManager) : IRequestHandler<UpdateEventCommand>
{
    public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await eventRepository.GetAsync(request.EventId);
        await eventManager.ChangeNameAsync(eventEntity, request.Name);
        var location = new Address(request.Street, request.District, request.City);
        eventEntity.Location = location;
        await eventManager.ChangeStartDateAsync(eventEntity, request.StartDate);
        eventEntity.EndDate = request.EndDate;
        await eventRepository.UpdateAsync(eventEntity, cancellationToken);
        //update image later
    }
}