using System;
using EjercicioFormacion.Utilities;
using EjercicioFormacion.Config;
using EjercicioFormacion.Resources;


namespace EjercicioFormacion
{
    public class ScheduleRecurringDaily : ScheduleBase
    {
        private readonly ScheduleRecurringDailyData data;
        private DateTime? lastExecutionTime;

        public ScheduleRecurringDaily(ScheduleData inputData) 
            : base(inputData)
        {
            if(inputData == null) throw new ArgumentNullException(nameof(inputData));
            if (inputData.RecurringDailyData == null) throw new ArgumentNullException(nameof(inputData.RecurringDailyData));            
            this.data = inputData.RecurringDailyData;
            if (this.data.DaysBetweenExecutions < 0) throw new FormatException("Days between executions must be bigger than 0");
            if (this.data.HoursBetweenExecutions < 0) throw new FormatException("Hours between executions must be bigger than 0");
            if (this.data.MinsBetweenExecutions < 0) throw new FormatException("Minutes between executions must be bigger than 0");
            if (this.data.SecsBetweenExecutions < 0) throw new FormatException("Seconds between executions must be bigger than 0");
        }
        private static DateTime CalculateNextExecutionTime(DateTime? lastExecutionTime, ScheduleRecurringDailyData inputData)
        {
            var currentDate = lastExecutionTime ?? inputData.CurrentDate;
            bool isInPeriod = currentDate.IsInPeriod(inputData.StartDate, inputData.EndDate);
            bool isInHour = currentDate.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour);

            DateTime nextExecutionTime = DateTime.MinValue;
            if (currentDate <= inputData.StartDate)
            {
                nextExecutionTime = inputData.StartDate.AddTicks(inputData.StartHour.Value.Ticks);
            }
            if (isInPeriod && isInHour)
            {
                nextExecutionTime = AddTime(currentDate, inputData, lastExecutionTime);
            }
            if (isInPeriod && isInHour == false && currentDate > inputData.StartDate && currentDate.TimeOfDay < inputData.StartHour)
            {
                nextExecutionTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day).AddTicks(inputData.StartHour.Value.Ticks);
            }
            if (isInPeriod && isInHour == false && inputData.CurrentDate > inputData.StartDate && inputData.CurrentDate.TimeOfDay > inputData.StartHour)
            {
                nextExecutionTime = AddTime(currentDate, inputData,null);
            }
            return nextExecutionTime;
        }
        private static bool IsInTime(DateTime time, ScheduleRecurringDailyData inputData)
        {
            return time.IsInPeriod(inputData.StartDate, inputData.EndDate) && time.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour);
        }

        private static DateTime AddTime(DateTime date, ScheduleRecurringDailyData inputData, DateTime? lastExecutionTime)
        {
            var newDate = date.AddSeconds(inputData.SecsBetweenExecutions)
                .AddMinutes(inputData.MinsBetweenExecutions)
                .AddHours(inputData.HoursBetweenExecutions);
            if (newDate.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour) == false)
            {
                newDate = newDate.AddDays(lastExecutionTime == null ? 1 : inputData.DaysBetweenExecutions);
                newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day);
                newDate = newDate.AddTicks(inputData.StartHour.Value.Ticks);
            }
            return newDate;
        }
        
        private static string GetDescription(DateTime? nextExecutionTime,ScheduleRecurringDailyData inputData)
        {            
            if (nextExecutionTime != null)
            {
                return string.Format(ScheduleRecurringDialyResources.Description,
                    inputData.DaysBetweenExecutions > 1 ?
                        inputData.DaysBetweenExecutions + " " + ScheduleRecurringDialyResources.Days : 
                        ScheduleRecurringDialyResources.Day,
                    inputData.StartHour,
                    inputData.EndHour,
                    nextExecutionTime.Value.ToString(),
                    inputData.StartDate.ToString());
            }
            return "Occurs Recurring Dialy. Schedule will not be used";
        }

        public override DateTime? GetNextExecutionTime(out string description)
        {
            if (this.Enabled == false)
            {
                description = GetDescription(null, this.data);
                return null;
            }
            DateTime? nextExecutionTime;
            try
            {
                nextExecutionTime = CalculateNextExecutionTime(this.lastExecutionTime, this.data);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("has exceeded the maximum allowed date value.", ex);
            }
            if (IsInTime(nextExecutionTime.Value, this.data))
            {
                description = GetDescription(nextExecutionTime, this.data);
                this.lastExecutionTime = nextExecutionTime;
                return nextExecutionTime;
            }
            description = GetDescription(null, this.data);
            return null;
        }
    }
}
