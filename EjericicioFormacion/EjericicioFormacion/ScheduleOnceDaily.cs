using System;
using EjercicioFormacion.Utilities;
using EjercicioFormacion.Config;
using EjercicioFormacion.Resources;

namespace EjercicioFormacion
{
    public class ScheduleOnceDaily:ScheduleBase
    {
        private readonly ScheduleOnceData _data;
        public ScheduleOnceDaily(ScheduleData InputData) 
            : base(InputData)
        {
            this._data = InputData.OnceData;
        }

        private bool MustBeRun()
        {
            return (base.Enabled == false ||
                _data.CurrentDate > _data.ProgrammedTime ||
                _data.ProgrammedTime.IsInPeriod(_data.StartDate, _data.EndDate) == false) == false;
        }
        private string GetDescription(DateTime? nextExecutionTime)
        {
            if (nextExecutionTime == null) return ScheduleOnceDialyResources.NullNextExecutionTimDescripcion;
            return string.Format(ScheduleOnceDialyResources.Description,
                nextExecutionTime.Value.ToString(), _data.StartDate.ToString());
        }

        public override DateTime? GetNextExecutionTime(out string description)
        {            
            if (!this.MustBeRun()) 
            {
                description = GetDescription(null);
                return null; 
            }
            description = GetDescription(_data.ProgrammedTime);
            return _data.ProgrammedTime;
        }        
    }
}
