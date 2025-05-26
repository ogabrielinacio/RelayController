using MediatR;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Aggregates.UserAggregates.Entities;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;
using RelayController.Domain.ValueObjects;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.BecomeOwner;

public class BecomeOwnerHandler(IUserBoardsRolesRepository userBoardsRolesRepository,IRelayControllerBoardRepository relayControllerBoardRepository ,IUnitOfWork unitOfWork) : IRequestHandler<BecomeOwnerCommand, bool>
{
    public async Task<bool> Handle(BecomeOwnerCommand command, CancellationToken cancellationToken)
    {
        var board = await relayControllerBoardRepository.GetByIdAsync(command.BoardId, cancellationToken);
        if (board == null)
        {
            throw new DomainNotFoundException("Board not found");
        }
        
        var alreadyHasAOwner = await  userBoardsRolesRepository.AlreadyHasAOwner(command.BoardId, cancellationToken);

        if (alreadyHasAOwner)
        {
            throw new DomainRuleViolationException($"Board with Id {command.BoardId} already has a Owner");
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