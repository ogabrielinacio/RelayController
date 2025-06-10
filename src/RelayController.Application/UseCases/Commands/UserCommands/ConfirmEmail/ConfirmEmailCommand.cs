using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.ConfirmEmail;

public record ConfirmEmailCommand() : IRequest
{
    public Guid Id { get; init; }
}