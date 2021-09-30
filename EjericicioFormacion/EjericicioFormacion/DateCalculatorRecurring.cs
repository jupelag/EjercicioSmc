using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjericicioFormacion
{
    public class DateCalculatorRecurring : DateCalculator
    {
        public DateCalculatorRecurring(DateTime CurrentDate, DateTime StartDate, DateTime? EndDate) 
            : base(CurrentDate, StartDate, EndDate)
        {
        }

        public override string GetDescription()
        {
            throw new NotImplementedException();
        }

        public override DateTime GetNextExecutionTime()
        {
            throw new NotImplementedException();
        }
    }
}
