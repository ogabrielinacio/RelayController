using FluentValidation;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleActivate;


public class ToggleActivateValidator: AbstractValidator<ToggleActivateCommand>
{
    public ToggleActivateValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
    }
}