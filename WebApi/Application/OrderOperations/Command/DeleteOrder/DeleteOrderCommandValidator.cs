using FluentValidation;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder;

public class DeleteOrderCommandValidator: AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty().GreaterThan(0);
    }
}