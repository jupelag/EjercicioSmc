using System;

namespace EjericicioFormacion
{
    public abstract class DateCalculator
    {
        protected readonly DateTime CurrentDate;
        protected readonly DateTime StartDate;
        protected readonly DateTime? EndDate;
        public DateCalculator(DataDateCalculator InputData)
        {
            this.CurrentDate = InputData.CurrentDate;
            this.StartDate = InputData.StartDate;
            this.EndDate = InputData.EndDate;
        }
        public bool Enabled { get; set; } = true;
        public abstract DateTime? GetNextExecutionTime();
        public abstract string GetDescription();
        
    }
}
