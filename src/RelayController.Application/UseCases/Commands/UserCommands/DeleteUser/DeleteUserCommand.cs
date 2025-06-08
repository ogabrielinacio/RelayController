using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.DeleteUser;

public record DeleteUserCommand :IRequest<bool>
{
   public Guid Id { get; init;} 
}