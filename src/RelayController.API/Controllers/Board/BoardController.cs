using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelayController.API.Common;
using RelayController.API.Controllers.Board.AddRoutine;
using RelayController.API.Controllers.Board.DeleteRoutine;
using RelayController.API.Controllers.Board.ToggleActivateRoutine;
using RelayController.Application.UseCases.Commands.BoardCommands.AddRoutine;
using RelayController.Application.UseCases.Commands.BoardCommands.DeleteRoutine;
using RelayController.Application.UseCases.Commands.BoardCommands.ToggleActivateRoutine;
using RelayController.Application.UseCases.Commands.BoardCommands.ToggleEnable;
using RelayController.Application.UseCases.Commands.BoardCommands.ToggleMode;
using RelayController.Application.UseCases.Queries.GetRelayControllerBoard;
using RelayController.Application.UseCases.Queries.UserBoardQueries.HasPermission;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Enums;
using RelayController.Infrastructure.Security;

namespace RelayController.API.Controllers.Board;

[Authorize(Policy = AuthorizationPolicies.RequireAuthentication)]
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
    public async Task<IActionResult> AddRoutine([FromBody] AddRoutineRequest request, CancellationToken cancellationToken)
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
    
    [HttpDelete("delete-routine")]
    public async Task<IActionResult> DeleteRoutine([FromBody] DeleteRoutineRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var permissionQuery = new HasPermissionQuery
        {
            UserId = userId,
            BoardId = request.BoardId
        };
        
        var hasPermission = await sender.Send(permissionQuery,cancellationToken);
        if (!hasPermission) {
            return BadRequest();
        }

        var command = new DeleteRoutineCommand
        {
            BoardId = request.BoardId,
            RoutineId = request.RoutineId
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("activate-routine")]
    public async Task<IActionResult> ActivateRoutine([FromBody] ToggleActivateRoutineRequest request ,CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var permissionQuery = new HasPermissionQuery
        {
            UserId = userId,
            BoardId = request.BoardId
        };
        
        var hasPermission = await sender.Send(permissionQuery,cancellationToken);
        if (!hasPermission) {
            return BadRequest();
        }
        
        var command = new ToggleActivateRoutineCommand
        {
            BoardId = request.BoardId,
            RoutineId = request.RoutineId,
            IsActive = true
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("deactivate-routine")]
    public async Task<IActionResult> DeactivateRoutine([FromBody] ToggleActivateRoutineRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var permissionQuery = new HasPermissionQuery
        {
            UserId = userId,
            BoardId = request.BoardId
        };
        
        var hasPermission = await sender.Send(permissionQuery,cancellationToken);
        if (!hasPermission) {
            return BadRequest();
        }
        
        var command = new ToggleActivateRoutineCommand
        {
            BoardId = request.BoardId,
            RoutineId = request.RoutineId,
            IsActive = false
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("{id:guid}/manual-mode")]
    public async Task<IActionResult> SetManualMode([FromRoute] Guid id,CancellationToken cancellationToken)
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
        
        var command = new ToggleModeCommand
        {
            Id = id,
            Mode = Mode.Manual
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("{id:guid}/auto-mode")]
    public async Task<IActionResult> SetAutoMode([FromRoute] Guid id,CancellationToken cancellationToken)
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
        
        var command = new ToggleModeCommand
        {
            Id = id,
            Mode = Mode.Auto 
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }
}