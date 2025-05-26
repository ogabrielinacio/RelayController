using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;
using RelayController.Domain.ValueObjects;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.DeleteUserFromDevice;

public class DeleteUserFromDeviceHandler(IUserBoardsRolesRepository userBoardsRolesRepository, IUserRepository userRepository ,IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserFromDeviceCommand, bool>
{
    public async Task<bool> Handle(DeleteUserFromDeviceCommand command, CancellationToken cancellationToken)
    {
        var hasOwnerPermission = await userBoardsRolesRepository.HasSpecificPermission(command.UserId, command.BoardId, Role.Owner, cancellationToken);
        
        if (!hasOwnerPermission)
        {
            throw new DomainForbiddenAccessException("Access denied: user doesn't have the owner permission."); 
        } 
        
        var userExists = await userRepository.GetByEmailAsync(command.Email, cancellationToken);
       
        if (userExists is null)
        {
            throw new DomainNotFoundException($"User with email {command.Email} doesn't exists"); 
        }
        
        var role = await userBoardsRolesRepository
            .GetByUserIdAndBoardId(userExists.Id, command.BoardId,cancellationToken);
        if (role == null)
        {
            throw new DomainNotFoundException("The relationship could not be found.");
        }

        await userBoardsRolesRepository.RemoveAsync(role, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}