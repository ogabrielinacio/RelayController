using RelayController.Application.Common.Enums;
using RelayController.Domain.Enums;
using RelayController.Domain.ValueObjects;

namespace RelayController.Application.UseCases.Queries.GetRelayControllerBoard;

public sealed class GetRelayControllerBoardResponse
{
    public Guid Id { get; init; }
    public bool IsActive { get; init; }
    public bool IsEnable { get; init; }
    public Mode Mode { get; init; }

    public List<RoutineResponse> Routines { get; init; } = [];

    public DateTime? UpdatedAt { get; init; }
    public DateTime? PowerStateChangedAt { get; init; }
}

public sealed class RoutineResponse
{
    public Guid Id { get; init; }

    public string StartTime { get; init; } = null!;
    public string? EndTime { get; init; }

    public Repeat Repeat { get; init; }
    public DayOfWeek? DayOfWeek { get; init; }
    public int? DayOfMonth { get; init; }

    public bool IsActive { get; init; }
}