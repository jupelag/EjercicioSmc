using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjericicioFormacion
{
    public class DateCalculatorOnce : DateCalculator
    {
        private DateTime NextExecutionTime;
        public DateCalculatorOnce(DateTime CurrentDate, DateTime StartDate, DateTime? EndDate, DateTime NextExecutionTime) 
            : base(CurrentDate, StartDate, EndDate)
        {
            this.NextExecutionTime = NextExecutionTime;
        }

        public override string GetDescription()
        {
            return string.Format("Ocurrs once. Schedule will be used on {0} at {2} starting on {3}",this.NextExecutionTime.Day,this.NextExecutionTime.Hour,base.StartDate);
        }

        public override DateTime GetNextExecutionTime() => this.NextExecutionTime;
        
            
        

    }
}
