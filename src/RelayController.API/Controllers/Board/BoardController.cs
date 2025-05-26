using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelayController.API.Common;
using RelayController.API.Controllers.Board.AddRoutine;
using RelayController.Application.UseCases.Commands.BoardCommands.AddRoutine;
using RelayController.Application.UseCases.Commands.BoardCommands.ToggleEnable;
using RelayController.Application.UseCases.Commands.BoardCommands.UpdateRelayControllerBoard;
using RelayController.Application.UseCases.Queries.GetRelayControllerBoard;
using RelayController.Application.UseCases.Queries.UserBoardQueries;
using RelayController.Application.UseCases.Queries.UserBoardQueries.HasPermission;
using RelayController.Domain.Aggregates.RelayControllerAggregates;

namespace RelayController.API.Controllers.Board;

[Authorize]
[Route("relay-controller")]
public class BoardController(ISender sender) :  AppController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var permissionQuery = new HasPermissionQuery
        {
            UserId = userId,
            BoardId = id
        };
        var hasPermission = await sender.Send(permissionQuery,cancellationToken);
        if (!hasPermission)
        {
            return Unauthorized();
        }
        
        var query = new GetRelayControllerBoardQuery()
        {
            Id = id
        };
        var response = await sender.Send(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost("{id:guid}/enable")]
    public async Task<IActionResult> EnableAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var permissionQuery = new HasPermissionQuery
        {
            UserId = userId,
            BoardId = id
        };
        var hasPermission = await sender.Send(permissionQuery,cancellationToken);
        if (!hasPermission)
        {
            return Unauthorized();
        }
        
        var command = new ToggleEnableCommand
        {
            Id = id,
            IsEnable = true 
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{id:guid}/disable")]
    public async Task<IActionResult> DisableAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var permissionQuery = new HasPermissionQuery
        {
            UserId = userId,
            BoardId = id
        };
        
        var hasPermission = await sender.Send(permissionQuery,cancellationToken);
        if (!hasPermission) {
            return Unauthorized();
        }
        
        var command = new ToggleEnableCommand
        {
            Id = id,
            IsEnable = false
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("add-routine")]
    public async Task<IActionResult> UpdateAsync([FromBody] AddRoutineRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var permissionQuery = new HasPermissionQuery
        {
            UserId = userId,
            BoardId = request.Id
        };
        
        var hasPermission = await sender.Send(permissionQuery,cancellationToken);
        if (!hasPermission) {
            return BadRequest();
        }

        var command = new AddRoutineCommand
        {
           Routine = new Routine(request.Id, request.StartTime, request.Repeat, request.EndTime)
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }
}