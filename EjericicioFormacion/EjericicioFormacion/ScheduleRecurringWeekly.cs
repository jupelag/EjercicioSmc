using System;
using System.Globalization;
using System.Linq;
using EjercicioFormacion.Utilities;
using EjericicioFormacion.Config;
using EjericicioFormacion.Enumerations;
using EjericicioFormacion.Resources;



namespace EjericicioFormacion
{
    public class ScheduleRecurringWeekly : ScheduleRecurring
    {
        private readonly int weeksBetweenExecutions;
        private readonly int hoursBetweenExecutions;
        private readonly int minsBetweenExecutions;
        private readonly int secsBetweenExecutions;
        private readonly DaysOfTheWeek executionDays;
        private readonly TimeSpan startHour;
        private readonly TimeSpan endHour;
        private DateTime startTime;
        private DateTime? nextExecutionTime;

        public ScheduleRecurringWeekly(ScheduleRecurringWeeklyData InputData)
            : base(InputData)
        {
            this.weeksBetweenExecutions = InputData.WeeksBetweenExecutions;
            this.hoursBetweenExecutions = InputData.HoursBetweenExecutions;
            this.minsBetweenExecutions = InputData.MinBetweenExecutions;
            this.secsBetweenExecutions = InputData.SecBetweenExecutions;
            this.startHour = InputData.StartHour ?? new TimeSpan();
            this.endHour = InputData.EndHour ?? TimeSpan.Parse("23:59");
            this.executionDays = InputData.ExecutionDays;
            this.CalculateStartTime();
        }
        private string executionDaysStr
        {
            get
            {
                return string.Join(", ", Enum.GetValues(typeof(DayOfWeek))
                                            .OfType<DayOfWeek>()
                                            .Where(D => IsInWeekDays(D))
                                            .Select(D => D.ToString()));
            }
        }
        private string NumberBetweenExecutions
        {
            get 
            {
                if (this.hoursBetweenExecutions > 0)
                {
                    return this.hoursBetweenExecutions + " " + ScheduleRecurringWeeklyResources.Hours;
                }
                if (this.minsBetweenExecutions > 0)
                {
                    return this.minsBetweenExecutions + " " + ScheduleRecurringWeeklyResources.Minutes;
                }
                if (this.secsBetweenExecutions > 0)
                {
                    return this.secsBetweenExecutions + " " + ScheduleRecurringWeeklyResources.Seconds;
                }
                return string.Empty;
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
            else if (base.CurrentDate.DayOfYear.Equals(base.StartDate.DayOfYear) && base.CurrentDate.TimeOfDay > this.startHour)
            {
                this.startTime = this.AddTime(this.CurrentDate);
            }
            if (this.IsInWeekDays(this.startTime.DayOfWeek) == false)
            {
                this.startTime = this.GetNextDayInWeekDays(this.startTime);
            }
        }
        private bool IsInTime(DateTime time)
        {
            return time.IsInPeriod(base.StartDate, base.EndDate) && time.TimeOfDay.IsInTime(this.startHour, this.endHour);
        }
        private bool IsInWeekDays(DayOfWeek day)
        {
            var dayOfTheWeek = this.GetDayOfTheWeek(day);
            return (this.executionDays & dayOfTheWeek) == dayOfTheWeek;
        }
        private DateTime GetNextDayInWeekDays(DateTime date)
        {
            DateTime nextDay = date.AddDays(1);
            while (IsInWeekDays(nextDay.DayOfWeek) == false)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
        private DaysOfTheWeek GetDayOfTheWeek(DayOfWeek day)
        {
            return Enum.GetValues(typeof(DaysOfTheWeek))
                .OfType<DaysOfTheWeek>()
                .FirstOrDefault(D => D.ToString().Equals(day.ToString()));
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
        private int GetWeekInYear(DateTime date)
        {            
            return CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, date.DayOfWeek);
        }
        private bool IsSameWeek(DateTime date1, DateTime date2)
        {
            return this.GetWeekInYear(date1) == this.GetWeekInYear(date2);
        }
        private DateTime GetFirstDayNextExecutionWeek(DateTime date)
        {            
            int currentWeek = this.GetWeekInYear(date);
            int nextWeek = currentWeek + this.weeksBetweenExecutions;
            var currentDay = date;
            while (currentWeek != nextWeek)
            {
                currentDay = currentDay.AddDays(1);
                currentWeek = this.GetWeekInYear(currentDay);
            }
            return currentDay;
        }
        private DateTime AddTime(DateTime date)
        {
            var newDate = date.AddSeconds(this.secsBetweenExecutions)
                .AddMinutes(this.minsBetweenExecutions)
                .AddHours(this.hoursBetweenExecutions);
            if (newDate.TimeOfDay.IsInTime(this.startHour, this.endHour) == false)
            {                
                newDate = this.GetNextDayInWeekDays(newDate);
                if (this.IsSameWeek(date, newDate) == false)
                {
                    newDate = this.GetFirstDayNextExecutionWeek(date);
                }
                if (this.IsInWeekDays(newDate.DayOfWeek) == false)
                {
                    newDate = this.GetNextDayInWeekDays(newDate);
                }
                newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day);
                newDate = newDate.AddTicks(this.startHour.Ticks);
            }
            return newDate;
        }

        private string GetDescription(DateTime? nextExecutionTime)
        {
            if (nextExecutionTime != null)
            {

                return string.Format(ScheduleRecurringWeeklyResources.Description,
                    this.weeksBetweenExecutions > 1 ? this.weeksBetweenExecutions + " " + ScheduleRecurringWeeklyResources.Weeks :
                        ScheduleRecurringWeeklyResources.Week,
                    this.executionDaysStr,
                    this.NumberBetweenExecutions,
                    this.startHour,
                    this.endHour,
                    base.StartDate.ToString("dd/MM/yyyy HH:mm"));
            }
            return "Occurs Recurring Weekly. Schedule will not be used";
        }

        public override DateTime? GetNextExecutionTime(out string description)
        {
            if (this.weeksBetweenExecutions < 0) throw new FormatException("Weeks between executions must be bigger than 0");
            if (this.hoursBetweenExecutions < 0) throw new FormatException("Hours between executions must be bigger than 0");
            if (this.minsBetweenExecutions < 0) throw new FormatException("Minutes between executions must be bigger than 0");
            if (this.secsBetweenExecutions < 0) throw new FormatException("Seconds between executions must be bigger than 0");
            if (this.Enabled == false)
            {
                description = this.GetDescription(null);
                return null;
            }
            this.CalculateNextExecutionTime();
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
