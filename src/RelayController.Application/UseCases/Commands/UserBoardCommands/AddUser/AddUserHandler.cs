using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Aggregates.UserAggregates.Entities;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;
using RelayController.Domain.ValueObjects;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.AddUser;

public class AddUserHandler(IUserRepository userRepository ,IUserBoardsRolesRepository userBoardsRolesRepository, IUnitOfWork unitOfWork) : IRequestHandler<AddUserCommand, bool>
{
    public async Task<bool> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var hasOwnerPermission = await userBoardsRolesRepository.HasSpecificPermission(command.RequestedUserId, command.BoardId, Role.Owner, cancellationToken);
        
        if (!hasOwnerPermission)
        {
           throw new UnauthorizedAccessException("You do not have permission to add a user to this board."); 
        }

        if (command.RoleId == Role.Owner.Id)
        {
            throw new DomainRuleViolationException("A Board can only have one owner."); 
        }
        
        var userExists = await userRepository.GetByEmailAsync(command.Email, cancellationToken);
       
        if (userExists is null)
        {
            throw new InvalidOperationException($"User with email {command.Email} doesn't exists"); 
        }
        
        var role = new UserBoardsRoles { 
            UserId = userExists.Id,
            RelayControllerBoardId = command.BoardId,
            Role = Role.FromId(command.RoleId)
        };

        await userBoardsRolesRepository.AddAsync(role ,cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}