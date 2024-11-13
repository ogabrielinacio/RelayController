using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Domain.ValueObjects
{
    public sealed class Time : ValueObject
    {       
        protected Time()
        {
        }
        
        public int Hour { get; private set;  }
        public int Minute { get; private set;}
        public int Second { get; private set;}

        public Time(int hour, int minute, int second)
        {
            if (hour < 0 || hour > 23)
            {
                throw new DomainRuleViolationException("Hour must be between 0 and 23.");
            }

            if (minute < 0 || minute > 59)
            {
                throw new DomainRuleViolationException("Minute must be between 0 and 59.");
            }

            if (second < 0 || second > 59)
            {
                throw new DomainRuleViolationException("Second must be between 0 and 59.");
            }

            Hour = hour;
            Minute = minute;
            Second = second;
        }

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(Hour, Minute, Second);
        }

        public override string ToString()
        {
            return $"{Hour:D2}:{Minute:D2}:{Second:D2}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hour;
            yield return Minute;
            yield return Second;
        }

        public static implicit operator TimeSpan(Time time)
        {
            return time.ToTimeSpan();
        }

        public static implicit operator Time((int hour, int minute, int second) time)
        {
            return new Time(time.hour, time.minute, time.second);
        }

        public static implicit operator string(Time time)
        {
            return time.ToString();
        }
    }
}