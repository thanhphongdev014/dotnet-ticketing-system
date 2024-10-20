using MediatR;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Services.EventService.Domain.Events;
using TicketSystem.Application.Identity;

namespace TicketingSystem.Services.EventService.Application.Events.Update;

public class UpdateEventCommand(
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

public class UpdateEventCommandHandler(
    IRepository<Event, Guid> eventRepository,
    EventManager eventManager,
    ICurrentUser currentUser) : IRequestHandler<UpdateEventCommand>
{
    public Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}