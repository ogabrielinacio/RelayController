namespace RelayController.API.Controllers.Board.DeleteRoutine;

public record DeleteRoutineRequest
{
   public Guid BoardId { get; init; } 
   public Guid RoutineId { get; init; } 
}