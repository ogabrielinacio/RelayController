namespace RelayController.Domain.Common;

public interface IEmailService
{
    Task SendEmailConfirmationAsync(string email, string emailToken, CancellationToken cancellationToken);

    Task SendPasswordRecoveryEmailAsync(string email, string recoveryToken,
        CancellationToken cancellationToken);
}
