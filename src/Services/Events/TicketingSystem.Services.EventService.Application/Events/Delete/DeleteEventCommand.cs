using MediatR;
using TicketingSystem.Domain.Repositories;
using TicketingSystem.Services.EventService.Domain.Events;

namespace TicketingSystem.Services.EventService.Application.Events.Delete;

public class DeleteEventCommand(
    Guid eventId) : IRequest
{
    public Guid EventId { get; set; } = eventId;
}

public class UpdateEventCommandHandler(
    IRepository<Event, Guid> eventRepository) : IRequestHandler<DeleteEventCommand>
{
    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await eventRepository.GetAsync(request.EventId);
        await eventRepository.DeleteAsync(eventEntity, cancellationToken);
        //delete image later
    }
}