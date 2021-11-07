using System;
using EjercicioFormacion.Utilities;
using EjericicioFormacion.Config;
using EjericicioFormacion.Resources;


namespace EjericicioFormacion
{
    public class ScheduleRecurringDialy : ScheduleRecurring
    {
        private readonly int daysBetweenExecutions;      
        private DateTime startTime;
        private DateTime? nextExecutionTime;
        
        public ScheduleRecurringDialy(ScheduleRecurringDialyData InputData) 
            : base(InputData)
        {
            this.daysBetweenExecutions = InputData.DaysBetweenExecutions;
            try
            {
                this.CalculateStartTime();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("has exceeded the maximum allowed date value.");
            }
        }
        private void CalculateStartTime()
        {
            bool isInPeriod = base.CurrentDate.IsInPeriod(base.StartDate, base.EndDate);
            bool isInHour = base.CurrentDate.TimeOfDay.IsInTime(this.startHour, this.endHour);
            if (base.CurrentDate <= base.StartDate)
            {
                this.startTime = base.StartDate.AddTicks(this.startHour.Ticks);

            }
            else if (isInPeriod && isInHour)
            {
                this.startTime = this.CurrentDate;
            }
            else if (isInPeriod && isInHour == false && base.CurrentDate > base.StartDate && base.CurrentDate.TimeOfDay < this.startHour)
            {
                this.startTime = new DateTime(base.CurrentDate.Year, base.CurrentDate.Month, base.CurrentDate.Day).AddTicks(this.startHour.Ticks);
            }
            else if (isInPeriod && isInHour == false && base.CurrentDate > base.StartDate && base.CurrentDate.TimeOfDay > this.startHour)
            {
                this.startTime = this.AddTime(this.CurrentDate);
            }           
        }
        private bool IsInTime(DateTime time)
        {
            return time.IsInPeriod(base.StartDate, base.EndDate) && time.TimeOfDay.IsInTime(this.startHour, this.endHour);
        }
        private void CalculateNextExecutionTime()
        {
            if (this.nextExecutionTime == null)
            {
                this.nextExecutionTime = this.startTime;
            }
            else
            {
                this.nextExecutionTime = this.AddTime(this.nextExecutionTime.Value);
            }            
        }

        private DateTime AddTime(DateTime date)
        {
            var newDate = date.AddSeconds(this.secsBetweenExecutions)
                .AddMinutes(this.minsBetweenExecutions)
                .AddHours(this.hoursBetweenExecutions);
            if (newDate.TimeOfDay.IsInTime(this.startHour, this.endHour) == false)
            {
                newDate = newDate.AddDays(this.nextExecutionTime == null ? 1 : this.daysBetweenExecutions);
                newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day);
                newDate = newDate.AddTicks(this.startHour.Ticks);
            }
            return newDate;
        }
        
        private string GetDescription(DateTime? nextExecutionTime)
        {            
            if (nextExecutionTime != null)
            {
                return string.Format(ScheduleRecurringDialyResources.Description,
                    this.daysBetweenExecutions > 1 ?
                        this.daysBetweenExecutions + " " + ScheduleRecurringDialyResources.Days : 
                        ScheduleRecurringDialyResources.Day,
                    this.startHour,
                    this.endHour,
                    nextExecutionTime.Value.ToString("dd/MM/yyyy"),
                    nextExecutionTime.Value.ToString("HH:mm"),
                    base.StartDate.ToString("dd/MM/yyyy HH:mm"));
            }
            return "Occurs Recurring Dialy. Schedule will not be used";
        }

        public override DateTime? GetNextExecutionTime(out string description)
        {            
            if (this.daysBetweenExecutions < 0) throw new FormatException("Days between executions must be bigger than 0");
            if (this.hoursBetweenExecutions < 0) throw new FormatException("Hours between executions must be bigger than 0");
            if (this.minsBetweenExecutions < 0) throw new FormatException("Minutes between executions must be bigger than 0");
            if (this.secsBetweenExecutions < 0) throw new FormatException("Seconds between executions must be bigger than 0");
            if (this.Enabled == false)
            {
                description = this.GetDescription(null);
                return null; 
            }
            try
            {
                this.CalculateNextExecutionTime();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("has exceeded the maximum allowed date value.");
            }
            if (this.IsInTime(this.nextExecutionTime.Value))
            {
                description = this.GetDescription(this.nextExecutionTime);
                return this.nextExecutionTime;
            }
            description = this.GetDescription(null);
            return null;
        }
    }
}
