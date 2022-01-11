using System;
using Xunit;
using FluentAssertions;
using EjercicioFormacion.Config;
using EjercicioFormacion;
using EjercicioFormacion.Enumerations;

namespace Test.Test
{
    public class ScheduleRecurringWeeklyTest
    {
        [Fact]
        public void ScheduleRecurringWeekly_Hours_CurrentDay_BiggerThan_StartDate_Serie_Return_Correct_Dates()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020,01,02),new DateTime(2020,01,01))
            {
                EndDate = new DateTime(2020,01,18),
                WeeksBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = DaysOfTheWeek.Monday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 13, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 13, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 13, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 16, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 16, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 16, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 17, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 17, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 17, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringWeekly_Hours_Serie_Return_Correct_Dates()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020,01,01), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 18),                
                WeeksBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = DaysOfTheWeek.Monday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 13, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 13, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 13, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 16, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 16, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 16, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 17, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 17, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 17, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringWeekly_Hours_Serie_Return_Correct_Message()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020,01,03),
                WeeksBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = DaysOfTheWeek.Monday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            string message;
            schedule.GetNextExecutionTime(out message);
            message.Should().Be("Ocurrs every 2 weeks on Monday, Thursday, Friday every 2 hours between 04:00:00 and 08:00:00 starting on 01/01/2020 0:00:00");
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out message);
            message.Should().Be("Occurs Recurring Weekly. Schedule will not be used");
        }
        [Fact]
        public void ScheduleRecurringWeekly_CurrentDateTimeMaxVaule_return_correct_exception()
        {
            var data = new ScheduleRecurringWeeklyData(DateTime.MaxValue, new DateTime(2020, 01, 01))
            {                
                WeeksBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            FluentActions.Invoking(() => schedule.GetNextExecutionTime(out _)).Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        [Fact]
        public void ScheduleRecurringWeekly_GoToDateTimeMaxVaule_return_correct_exception()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(9999,12,31), new DateTime(2020, 01, 01))
            {
                WeeksBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(04, 00, 00),
                ExecutionDays = DaysOfTheWeek.Monday | DaysOfTheWeek.Friday | DaysOfTheWeek.Saturday | DaysOfTheWeek.Sunday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Tuesday | DaysOfTheWeek.Wednesday
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _);
            FluentActions.Invoking(() =>schedule.GetNextExecutionTime(out _)).Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        [Fact]
        public void ScheduleRecurringWeekly_MinBetweenExecutions_Bigger_Than_Zero()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020,01,03),
                WeeksBetweenExecutions = 2,
                MinsBetweenExecutions = 30,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = DaysOfTheWeek.Monday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("02-01-2020 04:00"));
            schedule.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("02-01-2020 04:30"));
            schedule.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("02-01-2020 05:00"));
            schedule.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("02-01-2020 05:30"));
        }
        [Fact]
        public void ScheduleRecurringWeekly_SecBetweenExecutions_Bigger_Than_Zero_Return_Correct_Date()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 03),
                WeeksBetweenExecutions = 2,
                SecsBetweenExecutions = 7200,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = DaysOfTheWeek.Monday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 08, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringWeekly_CurrentDate_InPeriodInhOur_Return_Correct_Date()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020,01,01,04,00,00), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 03),
                WeeksBetweenExecutions = 2,
                SecsBetweenExecutions = 7200,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = (DaysOfTheWeek)127
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));            
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 08, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringWeekly_CurrentDate_Equals_StarDate_Bigger_endHour_Return_Correct_Date()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01, 10, 00, 00), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 03),
                WeeksBetweenExecutions = 2,
                SecsBetweenExecutions = 7200,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = (DaysOfTheWeek)127
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 08, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringWeekly_CurrentDate_Equals_StarDate_Bigger_endHour_Diferent_Day_Return_Correct_Date()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01, 10, 00, 00), new DateTime(2019, 12, 31))
            {
                EndDate = new DateTime(2020, 01, 03),
                WeeksBetweenExecutions = 2,
                SecsBetweenExecutions = 7200,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = (DaysOfTheWeek)127
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 08, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringWeekly_Enabled_False_Retun_null_and_Correct_Message()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01,10,00,00), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 03),
                WeeksBetweenExecutions = 2,
                SecsBetweenExecutions = 7200,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = (DaysOfTheWeek)127
            };
            var schedule = new ScheduleRecurringWeekly(new ScheduleData(data));
            schedule.Enabled = false;
            string message;
            schedule.GetNextExecutionTime(out message).Should().Be(null);
            message.Should().Be("Occurs Recurring Weekly. Schedule will not be used");
        }
        [Fact]
        public void ScheduleRecurringWeekly_HoursBetweenExecutions_Less_Than_Zero_Returnd_Correct_Exception()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01, 10, 00, 00), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 03),
                WeeksBetweenExecutions = 2,
                SecsBetweenExecutions = -1,
                HoursBetweenExecutions = -1,
                MinsBetweenExecutions = -1,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = (DaysOfTheWeek)127
            };            
            FluentActions.Invoking(() => new ScheduleRecurringWeekly(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringWeekly_MinsBetweenExecutions_Less_Than_Zero_Returnd_Correct_Exception()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01, 10, 00, 00), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 03),
                WeeksBetweenExecutions = 2,                                
                MinsBetweenExecutions = -1,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = (DaysOfTheWeek)127
            };            
            FluentActions.Invoking(() => new ScheduleRecurringWeekly(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringWeekly_SecsBetweenExecutions_Less_Than_Zero_Returnd_Correct_Exception()
        {
            var data = new ScheduleRecurringWeeklyData(new DateTime(2020, 01, 01, 10, 00, 00), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 03),
                WeeksBetweenExecutions = 2,
                SecsBetweenExecutions = -1,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
                ExecutionDays = (DaysOfTheWeek)127
            };            
            FluentActions.Invoking(() => new ScheduleRecurringWeekly(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }

    }
}
