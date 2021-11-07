using System;
using EjericicioFormacion.Config;

namespace EjericicioFormacion
{
    public abstract class ScheduleRecurring : Schedule
    {
        protected readonly int hoursBetweenExecutions;
        protected readonly int minsBetweenExecutions;
        protected readonly int secsBetweenExecutions;
        protected readonly TimeSpan startHour;
        protected readonly TimeSpan endHour;
        public ScheduleRecurring(ScheduleRecurringData InputData) 
            : base(InputData)
        {
            this.hoursBetweenExecutions = InputData.HoursBetweenExecutions;
            this.minsBetweenExecutions = InputData.MinBetweenExecutions;
            this.secsBetweenExecutions = InputData.SecBetweenExecutions;
            this.startHour = InputData.StartHour ?? new TimeSpan();
            this.endHour = InputData.EndHour ?? TimeSpan.Parse("23:59");
        }
    }
}
