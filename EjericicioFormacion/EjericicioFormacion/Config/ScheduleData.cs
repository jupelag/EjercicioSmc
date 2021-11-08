using System;
using EjercicioFormacion.Enumerations;

namespace EjercicioFormacion.Config
{
    public class ScheduleData
    {
        public ScheduleData(DateTime CurrentDate, DateTime StartDate)
        {
            this.CurrentDate = CurrentDate;
            this.StartDate = StartDate;
        }
        public DateTime CurrentDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class ScheduleOnceData : ScheduleData
    {
        public ScheduleOnceData(DateTime CurrentDate, DateTime StartDate) 
            : base(CurrentDate,StartDate)
        {
        }

        public DateTime ProgrammedTime { get; set; }
    }
    public class ScheduleRecurringData : ScheduleData
    {
        public ScheduleRecurringData(DateTime CurrentDate, DateTime StartDate) : base(CurrentDate, StartDate)
        {
        }
        public int HoursBetweenExecutions { get; set; }
        public int MinBetweenExecutions { get; set; }
        public int SecBetweenExecutions { get; set; }
        public TimeSpan? StartHour { get; set; }
        public TimeSpan? EndHour { get; set; }
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
