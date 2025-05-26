using MediatR;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.DeleteUserBoardRelationship;

public record DeleteUserBoardRelationshipCommand : IRequest<bool>
{
    public Guid UserId { get; init; }
    public Guid BoardId { get; init; }
}