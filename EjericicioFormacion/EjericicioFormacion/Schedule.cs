using System;
using EjercicioFormacion.Config;

namespace EjercicioFormacion
{
    public abstract class Schedule
    {
        protected readonly DateTime CurrentDate;
        protected readonly DateTime StartDate;
        protected readonly DateTime? EndDate;
        public Schedule(ScheduleData InputData)
        {
            if (InputData == null) throw new ArgumentNullException("Input data must not be null");
            this.CurrentDate = InputData.CurrentDate;
            this.StartDate = InputData.StartDate;
            this.EndDate = InputData.EndDate;
        }
        public bool Enabled { get; set; } = true;
        public abstract DateTime? GetNextExecutionTime(out string description);
        
    }
}
