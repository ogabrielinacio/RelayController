namespace RelayController.API.Controllers.Board.ToggleActivateRoutine;

public record ToggleActivateRoutineRequest
{
    public Guid BoardId { get; init; }
    
    public Guid RoutineId { get; init; } 
}