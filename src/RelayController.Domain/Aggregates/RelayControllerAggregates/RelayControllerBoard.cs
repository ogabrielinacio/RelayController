using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using RelayController.Domain.ValueObjects;

namespace RelayController.Domain.Aggregates.RelayControllerAggregates;

public class RelayControllerBoard : AuditableEntity, IAggregateRoot
{
    public bool IsActive { get; private set; }
    public bool IsEnable { get; private set; }
    
    public DateTime? PowerStateChangedAt { get; private set; }
    
    public Mode Mode { get; private set; } = Mode.Auto;
    
    private readonly List<Routine> _routines = new();
    public IReadOnlyCollection<Routine> Routines => _routines;
    
    protected RelayControllerBoard() { }
    
    public RelayControllerBoard(bool isActive, bool isEnable, DateTime startTime, Repeat repeat, DateTime? endTime)
    {
        IsActive = isActive;
        
        PowerStateChangedAt = DateTime.UtcNow;

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
    
    public Routine? GetRoutineById(Guid routineId)
    {
       return _routines.FirstOrDefault(r => r.Id == routineId);
    } 
    
    public bool AddRoutine(Routine routine)
    {
        if (_routines.Any(r => r.IsActive && routine.IsActive && r.ConflictsWith(routine)))
            return false;

        _routines.Add(routine);
        return true;
    }

    public bool RemoveRoutine(Guid routineId)
    {
        var routine =  GetRoutineById(routineId);
        if (routine is null)
            return false;
        
        _routines.Remove(routine);
        return true;

    }

    public bool DeactivateRoutine(Guid routineId)
    {
       var routine = GetRoutineById(routineId);
       if(routine is null)
           return false;
       routine.Deactivate();
       return true;
    }
    
    public bool ActivateRoutine(Guid routineId)
    {
       var routine = GetRoutineById(routineId);
       if(routine is null)
           return false;
       routine.Active();
       return true;
    }

    public void ActivateManualMode()
    {
        Mode = Mode.Manual;
    }
    
    public void ActivateAutoMode()
    {
        Mode = Mode.Auto;
    }
    
    public void ClearRoutines()
    {
        _routines.Clear();
    }

    public void Enable()
    {
        IsEnable = true;
        PowerStateChangedAt = DateTime.UtcNow;
    }

    private void Enable(DateTime startTime, Repeat repeat, DateTime? endTime = null)
    {
        IsEnable = true;
        PowerStateChangedAt = DateTime.UtcNow;

        var routine = new Routine(Id, startTime, repeat, endTime);
        AddRoutine(routine);
    }

    public void Disable()
    {
        IsEnable = false;
        PowerStateChangedAt = DateTime.UtcNow;
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

    private bool IsMustBeOnRoutine(DateTime currentDateTime)
    {
       var activeRoutine =  _routines
            .Where(r => r.MustBeOnRoutine(currentDateTime))
            .OrderByDescending(r => r.Created)
            .FirstOrDefault();
        
        return activeRoutine is not null;
    }

    public bool MustBeOn(DateTime currentDateTime)
    {
        if (!IsActive || IsEnable || Mode == Mode.Manual) return false;
        return IsMustBeOnRoutine(currentDateTime);
    }

    public bool MustBeOff(DateTime currentDateTime)
    {
        if (!IsActive || IsOff() || IsMustBeOnRoutine(currentDateTime) || Mode == Mode.Manual) return false;
        
        var activeRoutines = _routines
            .Where(r => r.MustBeOffRoutine(currentDateTime))
            .OrderByDescending(r => r.Created)
            .FirstOrDefault();
        return activeRoutines is not null;
    }
    
    public void RemoveExpiredRoutines(DateTime currentTime)
    {
        _routines.RemoveAll(r =>
            r.Repeat == Repeat.DoNoRepeat &&
            (
                (r.EndTime is not null && currentTime.TimeOfDay > r.EndTime.ToTimeSpan()) ||
                (r.EndTime is null && currentTime.TimeOfDay > r.StartTime.ToTimeSpan())
            )
        );
    }

}
