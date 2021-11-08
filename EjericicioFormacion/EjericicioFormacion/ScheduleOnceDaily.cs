using System;
using EjercicioFormacion.Utilities;
using EjercicioFormacion.Config;
using EjercicioFormacion.Resources;

namespace EjercicioFormacion
{
    public class ScheduleOnceDaily : ScheduleOnce
    {
        public ScheduleOnceDaily(ScheduleOnceData InputData) 
            : base(InputData)
        {
        }

        private bool MustBeRun()
        {
            return (base.Enabled == false ||
                base.CurrentDate > base.ProgrammedTime ||
                base.ProgrammedTime.IsInPeriod(base.StartDate, base.EndDate) == false) == false;
        }
        private string GetDescription(DateTime? nextExecutionTime)
        {
            if (nextExecutionTime == null) return ScheduleOnceDialyResources.NullNextExecutionTimDescripcion;
            return string.Format(ScheduleOnceDialyResources.Description,
                nextExecutionTime.Value.ToString(), base.StartDate.ToString());
        }

        public override DateTime? GetNextExecutionTime(out string description)
        {            
            if (!this.MustBeRun()) 
            {
                description = GetDescription(null);
                return null; 
            }
            description = GetDescription(this.ProgrammedTime);
            return this.ProgrammedTime;
        }        
    }
}
