using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjercicioFormacion.Config;

namespace EjercicioFormacion
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
