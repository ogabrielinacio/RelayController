using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.ResetPassword;

public class ResetPasswordHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.Id, cancellationToken)
                   ?? throw new DomainNotFoundException("User not found.");

        await userRepository.ChangePasswordAsync(user, command.NewPassword, cancellationToken);

        userRepository.Update(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}