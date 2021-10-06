using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjericicioFormacion
{
    public abstract class DateCalculatorOnce : DateCalculator
    {
        protected DateTime ProgrammedTime;
        public DateCalculatorOnce(DataDateCalculatorOnce InputData)
            : base(InputData)
        {
            this.ProgrammedTime = InputData.ProgrammedTime;
        }
    }
}
