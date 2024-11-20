using FluentValidation;

namespace RelayController.Application.UseCases.Commands.ToggleActivate;


public class ToggleActivateValidator: AbstractValidator<ToggleActivateCommand>
{
    public ToggleActivateValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
    }
}