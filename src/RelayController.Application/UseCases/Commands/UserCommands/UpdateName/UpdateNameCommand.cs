using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.UpdateName;

public record UpdateNameCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string NewName { get; init; } = string.Empty;
}