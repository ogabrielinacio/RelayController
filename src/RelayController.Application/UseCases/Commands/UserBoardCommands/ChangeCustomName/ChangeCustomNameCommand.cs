using MediatR;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.ChangeCustomName;

public sealed record ChangeCustomNameCommand : IRequest<bool>
{
    public Guid RequestedUserId { get; set; }

    public Guid BoardId { get; set; }
    public string NewName { get; set; } = string.Empty;
}