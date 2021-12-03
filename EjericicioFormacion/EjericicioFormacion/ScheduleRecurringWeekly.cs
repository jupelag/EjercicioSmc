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
        private DateTime? nextExecutionTime;        
        private readonly DateTime startTime;

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
            try
            {
                this.startTime = CalculateStartTime(this.data);
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("has exceeded the maximum allowed date value.");
            }
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
        private static DateTime CalculateStartTime(ScheduleRecurringWeeklyData inputData)
        {
            bool isInPeriod = inputData.CurrentDate.IsInPeriod(inputData.StartDate, inputData.EndDate);
            bool isInHour = inputData.CurrentDate.TimeOfDay.IsInTime(inputData.StartHour, inputData.EndHour);
            DateTime startTime = DateTime.MinValue;
            if (inputData.CurrentDate <= inputData.StartDate)
            {
                startTime = inputData.StartDate.AddTicks(inputData.StartHour.Value.Ticks);
            }
            if (isInPeriod && isInHour)
            {
                startTime = AddTime(inputData.CurrentDate, inputData);                 
            }
            if (isInPeriod && isInHour == false && inputData.CurrentDate > inputData.StartDate && inputData.CurrentDate.TimeOfDay < inputData.StartHour)
            {
                startTime = new DateTime(inputData.CurrentDate.Year, inputData.CurrentDate.Month, inputData.CurrentDate.Day).AddTicks(inputData.StartHour.Value.Ticks);
            }
            if (inputData.CurrentDate.DayOfYear.Equals(inputData.StartDate.DayOfYear) && inputData.CurrentDate.TimeOfDay > inputData.EndHour)
            {
                startTime = AddTime(inputData.CurrentDate,inputData);
            }
            if (IsInWeekDays(startTime.DayOfWeek,inputData) == false)
            {
                startTime = GetNextDayInWeekDays(startTime, inputData);
            }
            return startTime;
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
        private static DateTime? CalculateNextExecutionTime(DateTime startTime, DateTime? nextExecutionTime, ScheduleRecurringWeeklyData inputData)
        {
            return nextExecutionTime == null ? startTime : AddTime(nextExecutionTime.Value, inputData);
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
            try
            {
                this.nextExecutionTime = CalculateNextExecutionTime(this.startTime,this.nextExecutionTime,this.data);
            } catch (ArgumentOutOfRangeException)
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
