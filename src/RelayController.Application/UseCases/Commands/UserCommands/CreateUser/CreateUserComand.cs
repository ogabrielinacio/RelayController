using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.CreateUser;

public sealed record CreateUserCommand : IRequest<CreateUserResponse>
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}