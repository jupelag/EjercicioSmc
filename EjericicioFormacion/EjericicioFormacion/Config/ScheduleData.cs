using System;
using EjercicioFormacion.Enumerations;

namespace EjercicioFormacion.Config
{
    public class ScheduleData
    {
        public ScheduleData(ScheduleOnceData onceData)
        {            
            this.OnceData = onceData;
        }
        public ScheduleData(ScheduleRecurringDailyData recurringDailyData)
        {         
            this.RecurringDailyData = recurringDailyData;
        }
        public ScheduleData(ScheduleRecurringWeeklyData recurringWeeklyData)
        {         
            this.RecurringWeeklyData = recurringWeeklyData;
        }        
        public ScheduleOnceData OnceData { get; private set; }
        public ScheduleRecurringDailyData RecurringDailyData { get; private set; }
        public ScheduleRecurringWeeklyData RecurringWeeklyData { get; private set; }
    }
    public abstract class ScheduleCommonData
    {
        public ScheduleCommonData(DateTime CurrentDate, DateTime StartDate)
        {
            this.CurrentDate = CurrentDate;
            this.StartDate = StartDate;
        }
        public DateTime CurrentDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class ScheduleOnceData : ScheduleCommonData
    {
        public ScheduleOnceData(DateTime CurrentDate, DateTime StartDate) 
            : base(CurrentDate,StartDate)
        {
        }

        public DateTime ProgrammedTime { get; set; }
    }
    public class ScheduleRecurringData : ScheduleCommonData
    {
        public ScheduleRecurringData(DateTime CurrentDate, DateTime StartDate) : base(CurrentDate, StartDate)
        {
        }
        public int HoursBetweenExecutions { get; set; }
        public int MinsBetweenExecutions { get; set; }
        public int SecsBetweenExecutions { get; set; }
        public TimeSpan? StartHour { get; set; } = TimeSpan.Zero;
        public TimeSpan? EndHour { get; set; } = TimeSpan.Zero;
    }
    public class ScheduleRecurringDailyData : ScheduleRecurringData
    {
        public ScheduleRecurringDailyData(DateTime CurrentDate, DateTime StartDate) 
            : base(CurrentDate, StartDate)
        {
        }

        public int DaysBetweenExecutions { get; set; }

    }
    public class ScheduleRecurringWeeklyData : ScheduleRecurringData
    {
        public ScheduleRecurringWeeklyData(DateTime CurrentDate, DateTime StartDate) 
            : base(CurrentDate, StartDate)
        {
        }

        public int WeeksBetweenExecutions { get; set; }
        public DaysOfTheWeek ExecutionDays { get; set; }
    }
}
