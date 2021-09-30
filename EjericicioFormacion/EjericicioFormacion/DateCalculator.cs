using System;

namespace EjericicioFormacion
{
    public abstract class DateCalculator
    {
        protected readonly DateTime CurrentDate;
        protected readonly DateTime StartDate;
        protected readonly DateTime? EndDate;
        public DateCalculator(DateTime CurrentDate, DateTime StartDate, DateTime? EndDate)
        {
            this.CurrentDate = CurrentDate;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
        public abstract DateTime GetNextExecutionTime();
        public abstract string GetDescription();
        
    }
}
