using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjericicioFormacion.Config
{
    public class ScheduleData
    {
        public ScheduleData(DateTime CurrentDate, DateTime StartDate)
        {
            this.CurrentDate = CurrentDate;
            this.StartDate = StartDate;
        }
        public DateTime CurrentDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class ScheduleOnceData : ScheduleData
    {
        public ScheduleOnceData(DateTime CurrentDate, DateTime StartDate) 
            : base(CurrentDate,StartDate)
        {
        }

        public DateTime ProgrammedTime { get; set; }
    }   
    public class ScheduleRecurringDialyData : ScheduleData
    {
        public ScheduleRecurringDialyData(DateTime CurrentDate, DateTime StartDate) 
            : base(CurrentDate, StartDate)
        {
        }

        public int DaysBetweenExecutions { get; set; }
        public int HoursBetweenExecutions { get; set; }
        public int MinBetweenExecutions { get; set; }
        public int SecBetweenExecutions { get; set; }
        public TimeSpan? StartHour { get; set; }
        public TimeSpan? EndHour { get; set; }
    }
}
