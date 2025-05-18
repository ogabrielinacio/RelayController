using MediatR;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.BecomeOwner;

public sealed record BecomeOwnerCommand : IRequest<bool>
{
   public Guid UserId { get; set; }
   
   public Guid BoardId { get; set; }
}