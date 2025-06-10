using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.ResetPassword;

public record ResetPasswordCommand : IRequest
{
    public Guid Id { get; init; }
    public string NewPassword { get; init; } = string.Empty;
}