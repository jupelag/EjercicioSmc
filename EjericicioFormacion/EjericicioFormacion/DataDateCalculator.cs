using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjericicioFormacion
{
    public class DataDateCalculator
    {
        public DateTime CurrentDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class DataDateCalculatorOnce : DataDateCalculator
    {
        public DateTime ProgrammedTime { get; set; }
    }
    public class DataDateCalculatorRecurring : DataDateCalculator
    {
        public int DaysBetweenExecutions { get; set; }
    }
}
