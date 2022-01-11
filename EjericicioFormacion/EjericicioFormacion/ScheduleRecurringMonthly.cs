using System;
using System.Globalization;
using System.Linq;
using EjercicioFormacion.Utilities;
using EjercicioFormacion.Config;
using EjercicioFormacion.Enumerations;
using EjercicioFormacion.Resources;
using System.Collections.Generic;

namespace EjercicioFormacion
{
    public class ScheduleRecurringMonthly : ScheduleBase
    {
        private readonly ScheduleRecurringMonthlyData data;
        private DateTime? lastExecutionTime;
        private ScheduleRecurringDaily scheduleRecurringDialy;

        public ScheduleRecurringMonthly(ScheduleData inputData)
            : base(inputData)
        {
            if (inputData == null) throw new ArgumentNullException(nameof(inputData));
            if (inputData.RecurringMonthlyData == null) throw new ArgumentNullException(nameof(inputData.RecurringMonthlyData));
            this.data = inputData.RecurringMonthlyData;
            if (this.data.MonthsBetweenExecutions < 0) throw new FormatException("Weeks between executions must be bigger than 0");
            if (this.data.HoursBetweenExecutions < 0) throw new FormatException("Hours between executions must be bigger than 0");
            if (this.data.MinsBetweenExecutions < 0) throw new FormatException("Minutes between executions must be bigger than 0");
            if (this.data.SecsBetweenExecutions < 0) throw new FormatException("Seconds between executions must be bigger than 0");
        }
        private bool IsScheduleInExecution => this.scheduleRecurringDialy != null;
        private static MonthlyExecutionDays GetMonthExecution(DayOfWeek day)
        {
            return Enum.GetValues(typeof(MonthlyExecutionDays))
                .OfType<MonthlyExecutionDays>()
                .FirstOrDefault(D => D.ToString().Equals(day.ToString()));
        }
        private static bool IsInMonthExecutionDays(DayOfWeek day, ScheduleRecurringMonthlyData inputData)
        {
            var monthExecutionDay = GetMonthExecution(day);
            return (inputData.ExecutionDays & monthExecutionDay) == monthExecutionDay;
        }
        private static DateTime GetNextDayInMonthDays(DateTime date, ScheduleRecurringMonthlyData inputData)
        {
            DateTime nextDay = date.Date;
            nextDay = inputData.OrdinalDay == Ordinals.Last
                ? GetLastMonthDate(nextDay.Year, nextDay.Month, inputData)
                : CalculateNextDay(inputData, nextDay);

            if (nextDay.Month > date.Date.Month)
            {
                nextDay = GetFirstDateInNextMonth(date, inputData, nextDay);
                return GetNextDayInMonthDays(nextDay, inputData);
            }
            return nextDay;
        }

        private static DateTime CalculateNextDay(ScheduleRecurringMonthlyData inputData, DateTime date)
        {
            int days = 0;
            DateTime nextDay = date.Date;
            while (days != (int)inputData.OrdinalDay + 1)
            {
                if (days > 0)
                {
                    nextDay = nextDay.AddDays(1);
                }
                nextDay = GetNextDayInMonthExecutionDays(nextDay, inputData, false);
                days++;
            }
            return nextDay;
        }

        private static DateTime GetFirstDateInNextMonth(DateTime date, ScheduleRecurringMonthlyData inputData, DateTime nextDay)
        {
            nextDay = nextDay.AddMonths(inputData.MonthsBetweenExecutions - (nextDay.Month - date.Date.Month));
            nextDay = new DateTime(nextDay.Year, nextDay.Month, 1);
            return nextDay;
        }

        private static DateTime GetNextDayInMonthExecutionDays(DateTime day, ScheduleRecurringMonthlyData inputData, bool doReverseSearch)
        {
            var nextDate = day.Date;           
            while (IsInMonthExecutionDays(nextDate.DayOfWeek, inputData) == false)
            {
                nextDate = nextDate.AddDays(doReverseSearch ? -1:1);
            }
            return nextDate;
        }

        private static DateTime GetLastMonthDate(int year, int month, ScheduleRecurringMonthlyData inputData)
        {
            var date = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return GetNextDayInMonthExecutionDays(date,inputData, true);
        }

        private static string GetDescription(DateTime? date, ScheduleRecurringMonthlyData inputData)
        {
            if (date != null)
            {
                return $"Occurs the {inputData.OrdinalDay} {inputData.ExecutionDays} of every {inputData.MonthsBetweenExecutions} months every" +
                    $" {inputData.HoursBetweenExecutions} hours, {inputData.MinsBetweenExecutions} minutes, {inputData.SecsBetweenExecutions} seconds" +
                    $" between {inputData.StartHour} and {inputData.EndHour} starting on {inputData.StartDate}";
                
            }
            return "Occurs Recurring Monthly. Schedule will not be used";
        }
        public override DateTime? GetNextExecutionTime(out string description)
        {
            var currentDate = this.data.CurrentDate;
            DateTime? nextExecutionTime;
            
            if (this.IsScheduleInExecution)
            {
                nextExecutionTime = this.scheduleRecurringDialy.GetNextExecutionTime(out _);
                bool IsCurrentDayFinished = nextExecutionTime == null;
                if (!IsCurrentDayFinished)
                {
                    this.lastExecutionTime = nextExecutionTime;
                    description = GetDescription(nextExecutionTime, this.data);
                    return nextExecutionTime;
                }
                currentDate = GetDateChangingMonth();
            }

            DateTime nextDay = GetNextExecutionDay(currentDate);
            ScheduleRecurringDailyData recurringDailyData = GetScheduleRecurringData(nextDay);
            nextExecutionTime = ExecuteScheduleRecurringData(recurringDailyData);
            description = GetDescription(nextExecutionTime, this.data);
            return nextExecutionTime;
        }

        private DateTime GetNextExecutionDay(DateTime currentDate)
        {
            DateTime nextDay;
            if (IsInMonthExecutionDays(currentDate.DayOfWeek, data) && this.data.OrdinalDay == Ordinals.First)
            {
                nextDay = currentDate;
            }
            else
            {
                nextDay = GetNextDayInMonthDays(currentDate, data)
                    .AddTicks(currentDate.TimeOfDay.Ticks)
                    .AddHours(-this.data.HoursBetweenExecutions)
                    .AddMinutes(-this.data.MinsBetweenExecutions)
                    .AddSeconds(-this.data.MinsBetweenExecutions);
            }            
            return nextDay;
        }

        private DateTime GetDateChangingMonth()
        {
            var currentDate = new DateTime(this.lastExecutionTime.Value.Year,
                this.lastExecutionTime.Value.Month,
                1,
                this.data.StartHour.Value.Hours - this.data.HoursBetweenExecutions,
                this.data.StartHour.Value.Minutes,
                this.data.StartHour.Value.Seconds);
            currentDate = currentDate.AddMonths(this.data.MonthsBetweenExecutions);
            return currentDate;
        }

        private DateTime? ExecuteScheduleRecurringData(ScheduleRecurringDailyData recurringDailyData)
        {
            DateTime? nextExecutionTime;
            this.scheduleRecurringDialy = new ScheduleRecurringDaily(new ScheduleData(recurringDailyData));
            nextExecutionTime = this.scheduleRecurringDialy.GetNextExecutionTime(out _);
            if (nextExecutionTime == null)
            {
                recurringDailyData.EndDate = recurringDailyData.EndDate.Value.AddDays(1);
                this.scheduleRecurringDialy = new ScheduleRecurringDaily(new ScheduleData(recurringDailyData));
                nextExecutionTime = this.scheduleRecurringDialy.GetNextExecutionTime(out _);
            }
            return nextExecutionTime;
        }

        private ScheduleRecurringDailyData GetScheduleRecurringData(DateTime nextDay)
        {           
            return new ScheduleRecurringDailyData(nextDay, this.data.StartDate)
            {
                EndDate = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, 23, 59, 59),
                StartHour = this.data.StartHour,
                EndHour = this.data.EndHour,
                HoursBetweenExecutions = this.data.HoursBetweenExecutions,
                DaysBetweenExecutions = 1
            };
        }
    }
}