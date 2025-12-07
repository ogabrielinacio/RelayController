using RelayController.Application.Common.Enums;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Enums;
using RelayController.Domain.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace RelayController.Application.UseCases.Queries.GetRelayControllerBoard;


[Mapper]
public static partial class GetRelayControllerBoardMapper
{
    [MapProperty(
        source: nameof(RelayControllerBoard.PowerStateChangedAt),
        target: nameof(GetRelayControllerBoardResponse.PowerStateChangedAt)
    )]
    public static partial GetRelayControllerBoardResponse ToGetRelayControllerBoardResponse(
        this RelayControllerBoard board
    );

    private static List<RoutineResponse> MapRoutines(IReadOnlyCollection<Routine> routines)
    {
        return routines
            .OrderBy(r => r.Repeat)
            .ThenBy(r => r.StartTime.ToTimeSpan())
            .Select(r => new RoutineResponse
            {
                Id = r.Id,
                StartTime = r.StartTime.ToString(),
                EndTime = r.EndTime?.ToString(),
                Repeat = r.Repeat,
                DayOfWeek = r.DayOfWeek,
                DayOfMonth = r.DayOfMonth,
                IsActive = r.IsActive,
            })
            .ToList();
    }
}