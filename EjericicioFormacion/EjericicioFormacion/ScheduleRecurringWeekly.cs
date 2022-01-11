using System;
using System.Globalization;
using System.Linq;
using EjercicioFormacion.Utilities;
using EjercicioFormacion.Config;
using EjercicioFormacion.Enumerations;
using EjercicioFormacion.Resources;

namespace EjercicioFormacion
{
    public class ScheduleRecurringWeekly : ScheduleBase
    {
        private readonly ScheduleRecurringWeeklyData data;
        private DateTime? lastExecutionTime;

        public ScheduleRecurringWeekly(ScheduleData inputData)
            : base(inputData)
        {
            if(inputData == null) throw new ArgumentNullException(nameof(inputData));
            if (inputData.RecurringWeeklyData == null) throw new ArgumentNullException(nameof(inputData.RecurringWeeklyData));
            this.data = inputData.RecurringWeeklyData;
            if (this.data.WeeksBetweenExecutions < 0) throw new FormatException("Weeks between executions must be bigger than 0");
            if (this.data.HoursBetweenExecutions < 0) throw new FormatException("Hours between executions must be bigger than 0");
            if (this.data.MinsBetweenExecutions < 0) throw new FormatException("Minutes between executions must be bigger than 0");
            if (this.data.SecsBetweenExecutions < 0) throw new FormatException("Seconds between executions must be bigger than 0");
        }

        private static string GetexecutionDays(ScheduleRecurringWeeklyData inputData)
        {
            return string.Join(", ", Enum.GetValues(typeof(DayOfWeek))
                                        .OfType<DayOfWeek>()
                                        .Where(D => IsInWeekDays(D, inputData))
                                        .Select(D => D.ToString()));
        }
        private static string GetNumberBetweenExecutions(ScheduleRecurringWeeklyData inputData)
        {
                if (inputData.HoursBetweenExecutions > 0)
                {
                    return inputData.HoursBetweenExecutions + " " + ScheduleRecurringWeeklyResources.Hours;
                }
                else if (inputData.MinsBetweenExecutions > 0)
                {
                    return inputData.MinsBetweenExecutions + " " + ScheduleRecurringWeeklyResources.Minutes;
                }
                else if (inputData.SecsBetweenExecutions > 0)
                {
                    return inputData.SecsBetweenExecutions + " " + ScheduleRecurringWeeklyResources.Seconds;
                }
                else
                {
                    throw new ApplicationException("There must be a stipulated period between executions greater than or equal to zero");
                }            
        }
        private static DateTime CalculateNextExecutionTime(DateTime? lastExecutionTime, ScheduleRecurringWeeklyData inputData)
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
                nextExecutionTime = AddTime(currentDate, inputData);                 
            }
            if (isInPeriod && isInHour == false && currentDate > inputData.StartDate && currentDate.TimeOfDay < inputData.StartHour)
            {
                nextExecutionTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day).AddTicks(inputData.StartHour.Value.Ticks);
            }
            if (currentDate.TimeOfDay > inputData.EndHour)
            {
                nextExecutionTime = AddTime(currentDate,inputData);
            }
            if (IsInWeekDays(nextExecutionTime.DayOfWeek,inputData) == false)
            {
                nextExecutionTime = GetNextDayInWeekDays(nextExecutionTime, inputData);
            }
            return nextExecutionTime;
        }
        private static bool IsInTime(DateTime time, ScheduleRecurringWeeklyData inputData)
        {
            return time.IsInPeriod(inputData.StartDate, inputData.EndDate) && time.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour);
        }
        private static bool IsInWeekDays(DayOfWeek day, ScheduleRecurringWeeklyData inputData)
        {
            var dayOfTheWeek = GetDayOfTheWeek(day);
            return (inputData.ExecutionDays & dayOfTheWeek) == dayOfTheWeek;
        }
        private static DateTime GetNextDayInWeekDays(DateTime date, ScheduleRecurringWeeklyData inputData)
        {
            DateTime nextDay = date.AddDays(1);
            while (IsInWeekDays(nextDay.DayOfWeek, inputData) == false)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
        private static DaysOfTheWeek GetDayOfTheWeek(DayOfWeek day)
        {
            return Enum.GetValues(typeof(DaysOfTheWeek))
                .OfType<DaysOfTheWeek>()
                .FirstOrDefault(D => D.ToString().Equals(day.ToString()));
        }
        private static int GetWeekInYear(DateTime date)
        {            
            return CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, date.DayOfWeek);
        }
        private static bool IsSameWeek(DateTime date1, DateTime date2)
        {
            return GetWeekInYear(date1) == GetWeekInYear(date2);
        }
        private static DateTime GetFirstDayNextExecutionWeek(DateTime date, ScheduleRecurringWeeklyData inputData)
        {            
            int currentWeek = GetWeekInYear(date);
            int nextWeek = currentWeek + inputData.WeeksBetweenExecutions;
            var currentDay = date;
            while (currentWeek != nextWeek)
            {
                currentDay = currentDay.AddDays(1);
                currentWeek = GetWeekInYear(currentDay);
            }
            return currentDay;
        }
        private static DateTime AddTime(DateTime date, ScheduleRecurringWeeklyData inputData)
        {
            var newDate = date.AddSeconds(inputData.SecsBetweenExecutions)
                .AddMinutes(inputData.MinsBetweenExecutions)
                .AddHours(inputData.HoursBetweenExecutions);
            if (newDate.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour) == false)
            {                
                newDate = GetNextDayInWeekDays(newDate,inputData);
                if (IsSameWeek(date, newDate) == false)
                {
                    newDate = GetFirstDayNextExecutionWeek(date,inputData);
                }
                if (IsInWeekDays(newDate.DayOfWeek,inputData) == false)
                {
                    newDate = GetNextDayInWeekDays(newDate,inputData);
                }
                newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day);
                newDate = newDate.AddTicks(inputData.StartHour.Value.Ticks);
            }
            return newDate;
        }

        private static string GetDescription(DateTime? nextExecutionTime, ScheduleRecurringWeeklyData inputData)
        {
            if (nextExecutionTime != null)
            {

                return string.Format(ScheduleRecurringWeeklyResources.Description,
                    inputData.WeeksBetweenExecutions > 1 ? inputData.WeeksBetweenExecutions + " " + ScheduleRecurringWeeklyResources.Weeks :
                        ScheduleRecurringWeeklyResources.Week,
                    GetexecutionDays(inputData),
                    GetNumberBetweenExecutions(inputData),
                    inputData.StartHour,
                    inputData.EndHour,
                    inputData.StartDate.ToString());
            }
            return "Occurs Recurring Weekly. Schedule will not be used";
        }

        public override DateTime? GetNextExecutionTime(out string description)
        {
            if (this.Enabled == false)
            {
                description = GetDescription(null,this.data);
                return null;
            }
            DateTime? nextExecutionTime;
            try
            {
                nextExecutionTime  = CalculateNextExecutionTime(this.lastExecutionTime,this.data);
            }
            catch (ArgumentOutOfRangeException ex)
            {                
                throw new ArgumentOutOfRangeException("has exceeded the maximum allowed date value.",ex);
            }
            if (IsInTime(nextExecutionTime.Value,this.data))
            {
                description = GetDescription(nextExecutionTime,this.data);
                this.lastExecutionTime = nextExecutionTime;
                return nextExecutionTime;
            }
            description = GetDescription(null,this.data);
            return null;
        }
    }
}
