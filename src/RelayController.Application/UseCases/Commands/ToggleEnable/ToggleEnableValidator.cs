using FluentValidation;

namespace RelayController.Application.UseCases.Commands.ToggleEnable;


public class ToggleEnableValidator: AbstractValidator<ToggleEnableCommand>
{
    public ToggleEnableValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
    }
}