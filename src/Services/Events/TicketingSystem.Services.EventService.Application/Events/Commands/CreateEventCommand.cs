using MediatR;

namespace TicketingSystem.Services.EventService.Application.Events.Commands;

public class CreateEventCommand : IRequest
{
    
}
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
{
    public Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
