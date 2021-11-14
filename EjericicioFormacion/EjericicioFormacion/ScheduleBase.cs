using System;
using EjercicioFormacion.Config;

namespace EjercicioFormacion
{
    public abstract class ScheduleBase
    {
        public ScheduleBase(ScheduleData InputData)
        {
            if (InputData == null) throw new ArgumentNullException("Input data must not be null");
        }
        public bool Enabled { get; set; } = true;
        public abstract DateTime? GetNextExecutionTime(out string description);
        
    }
}
