using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.SendConfirmEmail;

public record SendConfirmEmailCommand() : IRequest
{
    public string Email { get; init; } = string.Empty;
}