using RelayController.Application.Common.Enums;

namespace RelayController.API.Controllers.Board.UpdateBoard;

public record UpdateBoardRequest
{
    public Guid Id { get; init; }
    public bool IsEnable { get; init; }
    public bool IsActive { get; init; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public RepeatDto Repeat { get; init; } 
}