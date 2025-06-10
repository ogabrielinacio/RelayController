using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.ConfirmEmail;

public class ConfirmEmailHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<ConfirmEmailCommand>
{
    public async Task Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.Id, cancellationToken);
        if (user is null)
            throw new DomainNotFoundException("User not found");
        
        user.ConfirmEmail();
        
        userRepository.Update(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken); 
    }
}