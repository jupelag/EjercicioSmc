using System;
using EjericicioFormacion.Config;

namespace EjericicioFormacion
{
    public abstract class Schedule
    {
        protected readonly DateTime CurrentDate;
        protected readonly DateTime StartDate;
        protected readonly DateTime? EndDate;
        public Schedule(ScheduleData InputData)
        {
            this.CurrentDate = InputData.CurrentDate;
            this.StartDate = InputData.StartDate;
            this.EndDate = InputData.EndDate;
        }
        public bool Enabled { get; set; } = true;
        public abstract DateTime? GetNextExecutionTime(out string description);
        
    }
}
