using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EjericicioFormacion.InputDateExceptions;

namespace EjericicioFormacion
{
    public static class DataDateCalculatorRecurringDialyValidator
    {
        public static int ValidateDays(DataDateCalculatorRecurring InputData)
        {
            if (InputData.DaysBetweenExecutions <= 0)
            {
                throw new DaysException("Input data days must be greater than zero");
            }
            return InputData.DaysBetweenExecutions;
        }
    }

}
