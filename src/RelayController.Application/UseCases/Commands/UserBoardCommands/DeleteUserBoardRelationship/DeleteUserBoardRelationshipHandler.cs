using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.DeleteUserBoardRelationship;

public class DeleteUserBoardRelationshipHandler(
    IUserBoardsRolesRepository userBoardsRolesRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteUserBoardRelationshipCommand, bool>
{
    public async Task<bool> Handle(DeleteUserBoardRelationshipCommand command, CancellationToken cancellationToken)
    {
        var role = await userBoardsRolesRepository
            .GetByUserIdAndBoardId(command.UserId, command.BoardId,cancellationToken);
        if (role == null)
        {
            throw new DomainNotFoundException("The relationship could not be found.");
        }

        await userBoardsRolesRepository.RemoveAsync(role, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}