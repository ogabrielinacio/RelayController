using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using RelayController.Domain.ValueObjects;

namespace RelayController.Domain.Aggregates.RelayControllerAggregates;

public class RelayControllerBoard : AuditableEntity, IAggregateRoot
{
    public bool IsActive { get; private set; }
    public bool IsEnable { get; private set; }
    public Time StartTime { get; private set; } = null!;
    public Time? EndTime { get; private set; }
    public Repeat Repeat { get; private set; }
    public DayOfWeek? DayOfWeek { get; private set; }
    public int? DayOfMonth { get; private set; }
    
    protected RelayControllerBoard() { }
    
    public RelayControllerBoard(bool isActive, bool isEnable, DateTime startTime, Repeat repeat, DateTime? endTime)
    {
        IsActive = isActive;

        if (isEnable && isActive)
        {
            Enable(startTime, repeat, endTime);
        }
        else
        {
            Disable();
        }
    }


    public void Update(DateTime startTime, Repeat repeat, DateTime? endTime, bool isEnable, bool isActive)
    {
        IsActive = isActive;

        if (isEnable && isActive)
        {
            Enable(startTime, repeat, endTime);
        }
        else
        {
            Disable();
        }
    }

    public void Enable()
    {
        IsEnable = true;
    }

    private void Enable(DateTime startTime, Repeat repeat, DateTime? endTime = null)
    {
        IsEnable = true;
        Repeat = repeat;

        if (endTime is not null)
        {
            var datetime = (DateTime)endTime;
            EndTime = new Time(datetime.Hour, datetime.Minute, datetime.Second);
        }

        switch (repeat)
        {
            case Repeat.Daily:
                RepeatDaily(startTime);
                break;

            case Repeat.Weekly:
                RepeatWeekly(startTime);
                break;

            case Repeat.Monthly:
                RepeatMonthly(startTime);
                break;

            case Repeat.DoNoRepeat:
            default:
                StartTime = new Time(startTime.Hour, startTime.Minute, startTime.Second);
                break;
        }
    }

    public void Disable()
    {
        IsEnable = false;
    }

    public void Active()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
    private bool IsOff() => !IsEnable;

    public bool MustBeOn(DateTime currentDateTime)
    {
        if (!IsActive || IsOff()) return false;

        return Repeat switch
        {
            Repeat.Daily => currentDateTime.TimeOfDay >= StartTime.ToTimeSpan(),
            Repeat.Weekly => currentDateTime.DayOfWeek == DayOfWeek &&
                             currentDateTime.TimeOfDay >= StartTime.ToTimeSpan(),
            Repeat.Monthly => currentDateTime.Day == DayOfMonth && currentDateTime.TimeOfDay >= StartTime.ToTimeSpan(),
            _ => false
        };
    }

    public bool MustBeOff(DateTime currentDateTime)
    {
        if (!IsActive || IsOff()) return true;

        if (EndTime is null) return false;

        return currentDateTime.TimeOfDay >= EndTime.ToTimeSpan();
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
}
