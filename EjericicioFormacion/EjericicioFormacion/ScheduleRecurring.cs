using System;
using EjericicioFormacion.Config;

namespace EjericicioFormacion
{
    public abstract class ScheduleRecurring : Schedule
    {        
        public ScheduleRecurring(ScheduleData InputData) 
            : base(InputData)
        {            
        }
    }
}
