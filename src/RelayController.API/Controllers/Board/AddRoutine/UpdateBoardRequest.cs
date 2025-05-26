using RelayController.Domain.Enums;

namespace RelayController.API.Controllers.Board.AddRoutine;

public record  AddRoutineRequest
{
    public Guid Id { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public Repeat Repeat { get; init; } 
}