using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.ChangeCustomName;

public class ChangeCustomNameHandler(IUserBoardsRolesRepository userBoardsRolesRepository , IUnitOfWork unitOfWork) : IRequestHandler<ChangeCustomNameCommand, bool>
{
    public async Task<bool> Handle(ChangeCustomNameCommand command, CancellationToken cancellationToken)
    {
        var hasOwnerPermission = await userBoardsRolesRepository.HasAnyPermission(command.RequestedUserId, command.BoardId, cancellationToken);
        
        if (!hasOwnerPermission)
        {
            throw new DomainForbiddenAccessException("You dont have permission to change the custom name."); 
        }

        var userBoardRole = await userBoardsRolesRepository.GetByUserIdAndBoardId(command.RequestedUserId, command.BoardId,
            cancellationToken);
        
        userBoardRole.CustomName = command.NewName;
        
        await userBoardsRolesRepository.UpdateAsync(userBoardRole, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return true; 
    }
}