using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjericicioFormacion.Config;

namespace EjericicioFormacion
{
    public abstract class ScheduleOnce : Schedule
    {
        protected DateTime ProgrammedTime;
        public ScheduleOnce(ScheduleOnceData InputData)
            : base(InputData)
        {
            this.ProgrammedTime = InputData.ProgrammedTime;
        }
    }
}
