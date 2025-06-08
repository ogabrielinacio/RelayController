using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelayController.API.Common;
using RelayController.API.Controllers.UserBoard.AddUserToDevice;
using RelayController.API.Controllers.UserBoard.BecomeOwner;
using RelayController.API.Controllers.UserBoard.ChangeCustomName;
using RelayController.API.Controllers.UserBoard.DeleteUserFromDevice;
using RelayController.Application.UseCases.Commands.UserBoardCommands.AddUser;
using RelayController.Application.UseCases.Commands.UserBoardCommands.BecomeOwner;
using RelayController.Application.UseCases.Commands.UserBoardCommands.ChangeCustomName;
using RelayController.Application.UseCases.Commands.UserBoardCommands.DeleteUserBoardRelationship;
using RelayController.Application.UseCases.Commands.UserBoardCommands.DeleteUserFromDevice;
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
    
    [HttpPut("rename")]
    public async Task<IActionResult> ChangeCustomName([FromBody] ChangeCustomNameRequest  request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        
        var command = new ChangeCustomNameCommand 
        {
            
            RequestedUserId = userId,
            BoardId = request.BoardId,
            NewName = request.NewName,
        };
        
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("remove/{boardId:guid}")]
    public async Task<IActionResult> RemoveRelationship([FromRoute] Guid boardId, CancellationToken cancellationToken)
    {
        
        var userId = GetCurrentUserId();
        var command = new DeleteUserBoardRelationshipCommand
        {
            UserId = userId,
            BoardId = boardId
        };
        
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }
    
    [HttpDelete("delete-user-from-device")]
    public async Task<IActionResult> DeleteUserFromDevice([FromBody] DeleteUserFromDeviceRequest request, CancellationToken cancellationToken)
    {
        
        var userId = GetCurrentUserId();
        var command = new DeleteUserFromDeviceCommand  
        {
            UserId = userId,
            BoardId = request.BoardId,
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
    