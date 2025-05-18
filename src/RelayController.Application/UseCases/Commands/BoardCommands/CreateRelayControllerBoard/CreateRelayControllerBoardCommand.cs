using RelayController.Application.Common.Enums;
using MediatR;

namespace RelayController.Application.UseCases.Commands.BoardCommands.CreateRelayControllerBoard;


public record CreateRelayControllerBoardCommand : IRequest<CreateRelayControllerBoardResponse>
{
    public bool IsEnable { get; init; }
    public bool IsActive { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public RepeatDto Repeat { get; init; }
}