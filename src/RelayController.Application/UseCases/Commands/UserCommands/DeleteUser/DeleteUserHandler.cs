using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.DeleteUser;

public class DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork): IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.Id, cancellationToken)
                   ?? throw new DomainNotFoundException("User not found.");
        
        await userRepository.DeleteAsync(user.Id, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}