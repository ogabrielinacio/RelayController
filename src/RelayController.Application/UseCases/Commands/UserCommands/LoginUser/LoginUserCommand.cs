using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.LoginUser;

public sealed record LoginUserCommand : IRequest<LoginUserResponse>
{
   public string Email { get; set; }
   public string Password { get; set; }
}