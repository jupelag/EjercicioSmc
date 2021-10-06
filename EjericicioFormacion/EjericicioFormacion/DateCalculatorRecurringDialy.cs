using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjericicioFormacion
{
    public class DateCalculatorRecurringDialy : DateCalculatorRecurring
    {
        private readonly int daysBetweenExecutions;
        
        public DateCalculatorRecurringDialy(DataDateCalculatorRecurring InputData) 
            : base(InputData)
        {
            this.daysBetweenExecutions = InputData.DaysBetweenExecutions;
        }
        private bool MustBeRun
        {
            get
            {                
                return (base.Enabled == false ||
                        base.CurrentDate < base.StartDate ||
                        base.CurrentDate > EndDate ||
                        this.nextExecutionTime > EndDate) == false;
            }
        }
        private DateTime nextExecutionTime
        {
            get 
            {
                var NewExecutionTime = base.CurrentDate;
                NewExecutionTime.AddDays(this.daysBetweenExecutions);
                return NewExecutionTime;
            }
        }
        public override string GetDescription()
        {
            var NextExecutiontime = this.GetNextExecutionTime();
            if (NextExecutiontime != null)
            {
                return string.Format("Ocurrs every day. Schedule will be used on {0} starting on {1}", NextExecutiontime.Value.Day, base.StartDate);
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
