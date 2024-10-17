using FluentValidation;

namespace TicketingSystem.Services.EventService.Application.Events.Add;

public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
{
    public AddEventCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Event name is empty");
    }
}