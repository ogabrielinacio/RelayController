using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.UpdateName;

public class UpdateNameHandler(IUserRepository userRepository ,IUnitOfWork unitOfWork) : IRequestHandler<UpdateNameCommand, bool>
{
    public async Task<bool> Handle(UpdateNameCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new DomainNotFoundException("User not found.");

        user.UpdateName(request.NewName);

       userRepository.Update(user, cancellationToken);
       await unitOfWork.CommitAsync(cancellationToken); 
       return true;
    }
}