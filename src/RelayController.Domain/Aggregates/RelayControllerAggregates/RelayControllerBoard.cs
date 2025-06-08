using RelayController.Domain.Common;
using RelayController.Domain.Enums;

namespace RelayController.Domain.Aggregates.RelayControllerAggregates;

public class RelayControllerBoard : AuditableEntity, IAggregateRoot
{
    public bool IsActive { get; private set; }
    public bool IsEnable { get; private set; }
    
    private readonly List<Routine> _routines = new();
    public IReadOnlyCollection<Routine> Routines => _routines;
    
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
    
    public bool AddRoutine(Routine routine)
    {
        if (_routines.Any(r => r.ConflictsWith(routine)))
            return false;
        _routines.Add(routine);
        return true;
    }

    public void RemoveRoutine(Guid routineId)
    {
        var routine = _routines.FirstOrDefault(r => r.Id == routineId);
        if (routine is not null)
            _routines.Remove(routine);
    }
    
    public void ClearRoutines()
    {
        _routines.Clear();
    }

    public void Enable()
    {
        IsEnable = true;
    }

    private void Enable(DateTime startTime, Repeat repeat, DateTime? endTime = null)
    {
        IsEnable = true;

        var routine = new Routine(Id, startTime, repeat, endTime);
        AddRoutine(routine);
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
        if (!IsActive || IsEnable) return false;
        var activeRoutines = _routines
                .Where(r => r.MustBeOnRoutine(currentDateTime))
                .OrderByDescending(r => r.Created)
                .FirstOrDefault();
        
        return _routines.Any(r => r.MustBeOnRoutine(currentDateTime));
    }

    public bool MustBeOff(DateTime currentDateTime)
    {
        if (!IsActive || IsOff()) return false;

        return _routines.Any(r => r.MustBeOffRoutine(currentDateTime));
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
