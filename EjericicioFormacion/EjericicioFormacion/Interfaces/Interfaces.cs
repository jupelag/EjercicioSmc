using EjercicioFormacion.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFormacion.Interfaces
{
    public class Interfaces
    {
        public interface ISchedule 
        {
            bool Enabled { get; set; }
            DateTime? GetNextExecutionTime(ScheduleData inputData,out string description);
        }
    }
}
