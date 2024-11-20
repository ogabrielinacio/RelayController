using MediatR;
using Microsoft.AspNetCore.Mvc;
using RelayController.Application.UseCases.Commands.CreateRelayControllerBoard;
using RelayController.Application.UseCases.Commands.ToggleActivate;
using RelayController.Application.UseCases.Commands.ToggleEnable;
using RelayController.Application.UseCases.Commands.UpdateRelayControllerBoard;
using RelayController.Application.UseCases.Queries.GetRelayControllerBoard;

namespace RelayController.API.Controllers;

[Route("relaycontroller")]
public class BoardController(ISender sender) : Controller
{

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRelayControllerBoardQuery()
        {
            Id = id
        };
        var response = await sender.Send(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRelayControllerBoardCommand command, CancellationToken cancellationToken)
    {
        var response = await sender.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPost("{id:guid}/enable")]
    public async Task<IActionResult> EnableAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new ToggleEnableCommand
        {
            Id = id,
            IsEnable = false
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{id:guid}/disable")]
    public async Task<IActionResult> DisableAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new ToggleEnableCommand
        {
            Id = id,
            IsEnable = false
        };
        await sender.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("{id:guid}/activate")]
    public async Task<IActionResult> ActivateAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new ToggleActivateCommand()
        {
            Id = id,
            IsActive = false
        };
        
        await sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new ToggleActivateCommand
        {
            Id = id,
            IsActive = false
        };
        
        await sender.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("{id:guid}/updated")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRelayControllerBoardCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command with { Id = id }, cancellationToken);
        return Ok();
    }
}