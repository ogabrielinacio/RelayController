using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelayController.API.Common;
using RelayController.API.Controllers.UserBoard.AddUserToDevice;
using RelayController.API.Controllers.UserBoard.BecomeOwner;
using RelayController.Application.UseCases.Commands.UserBoardCommands.AddUser;
using RelayController.Application.UseCases.Commands.UserBoardCommands.BecomeOwner;
using RelayController.Application.UseCases.Queries.UserBoardQueries.GetAllByUser;
using RelayController.Application.UseCases.Queries.UserBoardQueries.HasSpecificPermission;

namespace RelayController.API.Controllers.UserBoard;

[Authorize]
[Route("user-board")]
public class UserBoardController(ISender sender) : AppController
{
    [HttpPost("add")]
    public async Task<IActionResult> BecomeOwner([FromBody] BecomeOwnerRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var command = new BecomeOwnerCommand
        {
            UserId = userId,
            BoardId = request.BoardId,
            CustomName = request.CustomName,
        };
        
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost("add-user")]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        
        var command = new  AddUserCommand
        {
            
            RequestedUserId = userId,
            BoardId = request.BoardId,
            RoleId = request.RoleId,
            Email = request.Email,
        };
        
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllByUser(CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var command = new GetAllByUserQuery
        {
            UserId = userId,
        };
        
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

 
}
    