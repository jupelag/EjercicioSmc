using System;
using EjercicioFormacion.Utilities;
using EjericicioFormacion.Config;
using EjericicioFormacion.Resources;

namespace EjericicioFormacion
{
    public class ScheduleOnceDialy : ScheduleOnce
    {
        public ScheduleOnceDialy(ScheduleOnceData InputData) 
            : base(InputData)
        {
        }        

        private bool MustBeRun
        {
            get
            {
                return (base.Enabled == false ||
                    base.CurrentDate > base.ProgrammedTime ||
                    base.ProgrammedTime.IsInPeriod(base.StartDate, base.EndDate) == false) == false;
            }
        }
        private string GetDescription(DateTime? nextExecutionTime)
        {
            if (nextExecutionTime == null) return ScheduleOnceDialyResources.NullNextExecutionTimDescripcion;
            return string.Format(ScheduleOnceDialyResources.Description,
                nextExecutionTime.Value.ToString("dd/MM/yyyy"), nextExecutionTime.Value.ToString("HH:mm"), base.StartDate.ToString("dd/MM/yyyy HH:mm"));
        }

        public override DateTime? GetNextExecutionTime(out string description)
        {            
            if (this.MustBeRun == false) 
            {
                description = GetDescription(null);
                return null; 
            }
            description = GetDescription(this.ProgrammedTime);
            return this.ProgrammedTime;
        }        
    }
}
