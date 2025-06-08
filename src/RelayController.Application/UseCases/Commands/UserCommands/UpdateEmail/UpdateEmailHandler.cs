using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.UpdateEmail;

public class UpdateEmailHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateEmailCommand, bool>
{

    public async Task<bool> Handle(UpdateEmailCommand command, CancellationToken cancellationToken)
    {
       var user = await userRepository.GetByIdAsync(command.Id, cancellationToken)
                  ?? throw new DomainNotFoundException("User not found.");

       user.UpdateEmail(command.NewEmail);

       userRepository.Update(user, cancellationToken);
       await unitOfWork.CommitAsync(cancellationToken); 
       return true;
    }
}