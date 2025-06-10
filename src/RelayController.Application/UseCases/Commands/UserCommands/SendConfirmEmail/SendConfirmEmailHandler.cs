using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.SendConfirmEmail;

public class SendConfirmEmailHandler(IUserRepository userRepository, IEmailService emailService, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<SendConfirmEmailCommand>
{
    public async Task Handle(SendConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (user is null)
            throw new DomainNotFoundException("User not found");

        var emailToken = jwtTokenGenerator.GenerateToken(user, TokenPurpose.ConfirmEmail);
        await emailService.SendEmailConfirmationAsync(user.Email ,emailToken, cancellationToken);
    }
}