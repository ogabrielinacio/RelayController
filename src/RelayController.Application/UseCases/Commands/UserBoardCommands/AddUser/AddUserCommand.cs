using MediatR;
using RelayController.Domain.ValueObjects;

namespace RelayController.Application.UseCases.Commands.UserBoardCommands.AddUser;

public sealed record AddUserCommand : IRequest<bool>
{
    public Guid RequestedUserId { get; set; }
    
    public Guid BoardId { get; set; }
    
    public int RoleId { get; set; }
   
    public string Email { get; set; } = string.Empty;
}