using System;

namespace EjericicioFormacion
{
    public class DateCalculatorRecurringDialy : DateCalculatorRecurring
    {
        private readonly int daysBetweenExecutions;
        
        public DateCalculatorRecurringDialy(DataDateCalculatorRecurring InputData) 
            : base(InputData)
        {
            this.daysBetweenExecutions = DataDateCalculatorRecurringDialyValidator.ValidateDays(InputData);
        }
        private bool MustBeRun
        {
            get
            {
                if (base.Enabled == false) { return false; }
                return (base.Enabled == false ||
                        this.nextExecutionTime < base.StartDate ||
                        (base.EndDate != null && this.nextExecutionTime > base.EndDate)) == false;
            }
        }
        private DateTime nextExecutionTime => base.CurrentDate.AddDays(this.daysBetweenExecutions);

        public override string GetDescription()
        {
            var NextExecutiontime = this.GetNextExecutionTime();
            if (NextExecutiontime != null)
            {
                return string.Format(Resources.DateCalculatorRecurringDialyResources.Description,
                    this.daysBetweenExecutions > 1 ?
                        this.daysBetweenExecutions + " " + Resources.DateCalculatorRecurringDialyResources.Days : 
                        Resources.DateCalculatorRecurringDialyResources.Day,
                    NextExecutiontime.Value.ToString("dd/MM/yyyy"),
                    NextExecutiontime.Value.ToString("HH:mm"),
                    base.StartDate.ToString("dd/MM/yyyy HH:mm"));
            }
            return "Occurs once. Schedule will not be used";
        }

        public override DateTime? GetNextExecutionTime()
        {
            if (this.MustBeRun == false) { return null; }
            return this.nextExecutionTime;
        }
    }
}
