using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommandValidator: AbstractValidator<UpdateActorCommand>
{
    public UpdateActorCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(2).When(x => x.Model.Name != string.Empty);
        RuleFor(command => command.Model.Surname).MinimumLength(2).When(x => x.Model.Surname != string.Empty);
        RuleFor(command => command.ActorId).GreaterThan(0);
    }
}