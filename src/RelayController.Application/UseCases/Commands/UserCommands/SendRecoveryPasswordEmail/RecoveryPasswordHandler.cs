using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.SendRecoveryPasswordEmail;

public class SendRecoveryPasswordEmailHandler(IUserRepository userRepository, IEmailService emailService, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<SendRecoveryPasswordEmailCommand>
{
    public async Task Handle(SendRecoveryPasswordEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (user is null)
            throw new DomainNotFoundException("User not found");

        var recoveryToken = jwtTokenGenerator.GenerateToken(user, TokenPurpose.ResetPassword);
        await emailService.SendPasswordRecoveryEmailAsync(user.Email ,recoveryToken, cancellationToken);
    }
}