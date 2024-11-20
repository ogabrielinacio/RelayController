using RelayController.Application.Common.Enums;
using RelayController.Domain.ValueObjects;

namespace RelayController.Application.UseCases.Queries.GetRelayControllerBoard;

public sealed class GetRelayControllerBoardResponse
{
    public Guid Id { get; init; }
    public DateTime Created { get; init; }
    public DateTime? Updated { get; init; }
    public bool IsActive { get; init; }
    public bool IsEnable { get; init; }
    public string StartTime { get; init; } = null!;
    public string? EndTime { get; init; }
    public RepeatDTO Repeat { get; init; }
    public DayOfWeek? DayOfWeek { get; init; }
    public int? DayOfMonth { get; init; }
}