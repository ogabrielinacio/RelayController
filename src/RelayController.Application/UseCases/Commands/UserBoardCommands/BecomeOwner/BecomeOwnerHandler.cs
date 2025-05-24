using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Aggregates.UserAggregates.Entities;
using RelayController.Domain.Common;
using RelayController.Domain.ValueObjects;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.BecomeOwner;

public class BecomeOwnerHandler(IUserBoardsRolesRepository userBoardsRolesRepository, IUnitOfWork unitOfWork) : IRequestHandler<BecomeOwnerCommand, bool>
{
    public async Task<bool> Handle(BecomeOwnerCommand command, CancellationToken cancellationToken)
    {
        var alreadyHasAOwner = await  userBoardsRolesRepository.AlreadyHasAOwner(command.BoardId, cancellationToken);

        if (alreadyHasAOwner)
        {
            throw new InvalidOperationException($"Board with Id {command.BoardId} already has a Owner");
        }
        
        var role = new UserBoardsRoles { 
            UserId = command.UserId,
            RelayControllerBoardId = command.BoardId,
            CustomName = command.CustomName,
            Role = Role.Owner
        };

        await userBoardsRolesRepository.AddAsync(role ,cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}