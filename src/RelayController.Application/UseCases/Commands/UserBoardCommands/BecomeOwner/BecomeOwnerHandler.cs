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
        var role = new UserBoardsRoles { 
            UserId = command.UserId,
            RelayControllerBoardId = command.BoardId,
            Role = Role.Owner
        };

        await userBoardsRolesRepository.AddAsync(role ,cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}