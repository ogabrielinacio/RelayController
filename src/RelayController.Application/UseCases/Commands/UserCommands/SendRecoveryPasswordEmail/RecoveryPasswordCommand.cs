using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.SendRecoveryPasswordEmail;

public record SendRecoveryPasswordEmailCommand() : IRequest
{
    public string Email { get; init; } = string.Empty;
}