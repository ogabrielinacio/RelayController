using System.Text.Json.Serialization;
using RelayController.Application.Common.Enums;
using MediatR;

namespace RelayController.Application.UseCases.Commands.BoardCommands.UpdateRelayControllerBoard;

public record UpdateRelayControllerBoardCommand : IRequest
{
    public Guid Id { get; init; }
    public bool IsEnable { get; init; }
    public bool IsActive { get; init; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public RepeatDto Repeat { get; init; }
}