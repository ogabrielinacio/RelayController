using RelayController.Application.Common.Enums;
using MediatR;

namespace RelayController.Application.UseCases.Commands.CreateRelayControllerBoard;


public record CreateRelayControllerBoardCommand : IRequest<CreateRelayControllerBoardResponse>
{
    public bool IsEnable { get; init; }
    public bool IsActive { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public RepeatDTO Repeat { get; init; }
}