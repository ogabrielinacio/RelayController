using MediatR;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.DeleteUserFromDevice;

public record DeleteUserFromDeviceCommand : IRequest<bool>
{
    public Guid UserId { get; init; }
    public Guid BoardId { get; init; }
    public string Email { get; init; } = string.Empty;
};