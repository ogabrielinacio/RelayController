using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using RelayController.Domain.ValueObjects;

namespace RelayController.Domain.Aggregates.RelayControllerAggregates;

public class Routine : AuditableEntity 
{
    public Guid RelayControllerBoardId { get; private set; }
    public Time StartTime { get; private set; } = null!;
    public Time? EndTime { get; private set; }
    public Repeat Repeat { get; private set; }
    public DayOfWeek? DayOfWeek { get; private set; }
    public int? DayOfMonth { get; private set; }
    public bool IsActive { get; private set; } = true;

    protected Routine() { }

    public Routine(Guid relayControllerBoardId, DateTime startTime, Repeat repeat, DateTime? endTime)
    {
        RelayControllerBoardId = relayControllerBoardId;
        Repeat = repeat;

        StartTime = new Time(startTime.Hour, startTime.Minute, startTime.Second);
        EndTime = endTime is not null ? new Time(endTime.Value.Hour, endTime.Value.Minute, endTime.Value.Second) : null;

        switch (repeat)
        {
            case Repeat.Weekly:
                DayOfWeek = startTime.DayOfWeek;
                break;
            case Repeat.Monthly:
                DayOfMonth = startTime.Day;
                break;
        }
    }
    public bool MustBeOnRoutine(DateTime currentDateTime)
    {
        bool IsWithinRange()
        {
            var now = currentDateTime.TimeOfDay;

            if (EndTime is null) return now >= StartTime.ToTimeSpan();
            return now >= StartTime.ToTimeSpan() && now <= EndTime.ToTimeSpan();
        }

        if (!IsActive) return false;
        
        return Repeat switch
        {
            Repeat.Weekly => currentDateTime.DayOfWeek == DayOfWeek &&  IsWithinRange(),
            Repeat.Monthly => currentDateTime.Day == DayOfMonth && IsWithinRange(),
            _ => IsWithinRange()
        };
    }

    public bool MustBeOffRoutine(DateTime currentDateTime)
    {
        if (!IsActive) return false;
        
        if (EndTime is null) return false;

        var off = currentDateTime.TimeOfDay >= EndTime.ToTimeSpan();
        return Repeat switch {
            Repeat.Weekly =>  currentDateTime.DayOfWeek == DayOfWeek && off,
            Repeat.Monthly =>  currentDateTime.Day == DayOfMonth && off,
            _ => off
        };
    }
    
    public void Active()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void RepeatDaily(DateTime startTime)
    {
        StartTime = new Time(startTime.Hour, startTime.Minute, startTime.Second);
        DayOfWeek = null;
        DayOfMonth = null;
    }

    private void RepeatWeekly(DateTime startTime)
    {
        DayOfWeek = startTime.DayOfWeek;
        StartTime = new Time(startTime.Hour, startTime.Minute, startTime.Second);
        DayOfMonth = null;
    }

    private void RepeatMonthly(DateTime startTime)
    {
        DayOfMonth = startTime.Day;
        StartTime = new Time(startTime.Hour, startTime.Minute, startTime.Second);
        DayOfWeek = null;
    }
    
    public bool ConflictsWith(Routine other)
    {
        if (!IsActive || !other.IsActive)
            return false;
        
        if (Repeat != other.Repeat)
            return false;

        if (Repeat == Repeat.Daily || 
            (Repeat == Repeat.Weekly && DayOfWeek == other.DayOfWeek) || 
            (Repeat == Repeat.Monthly && DayOfMonth == other.DayOfMonth))
        {
            var thisStart = StartTime.ToTimeSpan();
            var thisEnd = EndTime?.ToTimeSpan() ?? thisStart;

            var otherStart = other.StartTime.ToTimeSpan();
            var otherEnd = other.EndTime?.ToTimeSpan() ?? otherStart;

            var overlap = thisStart < otherEnd && otherStart < thisEnd;
            return overlap;
        }

        return false;
    }

}
