﻿using FluentValidation;

namespace RelayController.Application.UseCases.Commands.BoardCommands.UpdateRelayControllerBoard;

public class UpdateRelayControllerBoardValidator: AbstractValidator<UpdateRelayControllerBoardCommand>
{
    public UpdateRelayControllerBoardValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
        RuleFor(p => p.Repeat)
            .IsInEnum();
        
    }
}