using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.UpdatePassword;

public class UpdatePasswordHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdatePasswordCommand, bool>
{

    public async Task<bool> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.Id, cancellationToken)
                   ?? throw new DomainNotFoundException("User not found.");

        var isPasswordValid =  await userRepository.VerifyPasswordAsync(user, command.Password, cancellationToken);
        if (!isPasswordValid)
            throw new DomainForbiddenAccessException("Invalid password.");
        await userRepository.ChangePasswordAsync(user, command.NewPassword, cancellationToken);

        userRepository.Update(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}