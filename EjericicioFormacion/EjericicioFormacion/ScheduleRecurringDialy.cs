using System;
using EjercicioFormacion.Utilities;
using EjercicioFormacion.Config;
using EjercicioFormacion.Resources;


namespace EjercicioFormacion
{
    public class ScheduleRecurringDaily : ScheduleBase
    {
        private readonly ScheduleRecurringDailyData data;
        private readonly DateTime startTime;
        private DateTime? nextExecutionTime;
        
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
            try
            {
                this.startTime = CalculateStartTime(inputData.RecurringDailyData);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("has exceeded the maximum allowed date value.");
            }
        }
        private static DateTime CalculateStartTime(ScheduleRecurringDailyData inputData)
        {
            bool isInPeriod = inputData.CurrentDate.IsInPeriod(inputData.StartDate, inputData.EndDate);
            bool isInHour = inputData.CurrentDate.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour);

            if (inputData.CurrentDate <= inputData.StartDate)
            {
                return inputData.StartDate.AddTicks(inputData.StartHour.Value.Ticks);
            }
            if (isInPeriod && isInHour)
            {
                return AddTime(inputData.CurrentDate, inputData, null);
            }
            if (isInPeriod && isInHour == false && inputData.CurrentDate > inputData.StartDate && inputData.CurrentDate.TimeOfDay < inputData.StartHour)
            {
                return new DateTime(inputData.CurrentDate.Year, inputData.CurrentDate.Month, inputData.CurrentDate.Day).AddTicks(inputData.StartHour.Value.Ticks);
            }
            if (isInPeriod && isInHour == false && inputData.CurrentDate > inputData.StartDate && inputData.CurrentDate.TimeOfDay > inputData.StartHour)
            {
                return AddTime(inputData.CurrentDate,inputData,null);
            }
            return DateTime.MinValue;
        }
        private static bool IsInTime(DateTime time, ScheduleRecurringDailyData inputData)
        {
            return time.IsInPeriod(inputData.StartDate, inputData.EndDate) && time.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour);
        }
        private static DateTime? CalculateNextExecutionTime(DateTime startTime, DateTime? nextExecutionTime, ScheduleRecurringDailyData inputData)
        {
            return nextExecutionTime == null ? startTime : AddTime(nextExecutionTime.Value, inputData, nextExecutionTime.Value);
        }

        private static DateTime AddTime(DateTime date, ScheduleRecurringDailyData inputData, DateTime? nextExecutionTime)
        {
            var newDate = date.AddSeconds(inputData.SecsBetweenExecutions)
                .AddMinutes(inputData.MinsBetweenExecutions)
                .AddHours(inputData.HoursBetweenExecutions);
            if (newDate.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour) == false)
            {
                newDate = newDate.AddDays(nextExecutionTime == null ? 1 : inputData.DaysBetweenExecutions);
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
                description = GetDescription(null,this.data);
                return null; 
            }
            try
            {
                this.nextExecutionTime = CalculateNextExecutionTime(this.startTime, this.nextExecutionTime, this.data);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("has exceeded the maximum allowed date value.");
            }
            if (IsInTime(this.nextExecutionTime.Value,this.data))
            {
                description = GetDescription(this.nextExecutionTime,this.data);
                return this.nextExecutionTime;
            }
            description = GetDescription(null,this.data);
            return null;
        }
    }
}
