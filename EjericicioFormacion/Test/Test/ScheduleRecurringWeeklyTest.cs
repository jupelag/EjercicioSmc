using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using EjericicioFormacion.Config;
using EjericicioFormacion;
using EjericicioFormacion.Enumerations;

namespace Test.Test
{
    public class ScheduleRecurringWeeklyTest
    {
        [Fact]
        public void ScheduleRecurringWeekly_Hours_Serie_Return_Correct_Dates()
        {
            var Data = new ScheduleRecurringWeeklyData(DateTime.Parse("01-01-2020"), DateTime.Parse("01-01-2020"))
            {
                EndDate = DateTime.Parse("18-1-2020"),
                WeeksBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = TimeSpan.Parse("04:00"),
                EndHour = TimeSpan.Parse("08:00"),
                ExecutionDays = DaysOfTheWeek.Monday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
            };
            var calculator = new ScheduleRecurringWeekly(Data);
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("02-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("02-01-2020 06:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("02-01-2020 08:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 06:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 08:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("13-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("13-01-2020 06:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("13-01-2020 08:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("16-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("16-01-2020 06:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("16-01-2020 08:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("17-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("17-01-2020 06:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("17-01-2020 08:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringWeekly_Hours_Serie_Return_Correct_Message()
        {
            var Data = new ScheduleRecurringWeeklyData(DateTime.Parse("01-01-2020"), DateTime.Parse("01-01-2020"))
            {
                EndDate = DateTime.Parse("3-1-2020"),
                WeeksBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = TimeSpan.Parse("04:00"),
                EndHour = TimeSpan.Parse("08:00"),
                ExecutionDays = DaysOfTheWeek.Monday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
            };
            var calculator = new ScheduleRecurringWeekly(Data);
            string message;
            calculator.GetNextExecutionTime(out message);
            message.Should().Be("Ocurrs every 2 weeks on Monday, Thursday, Friday every 2 hours between 04:00:00 and 08:00:00 starting on 01/01/2020 00:00");
            calculator.GetNextExecutionTime(out _);
            calculator.GetNextExecutionTime(out _);
            calculator.GetNextExecutionTime(out message);
            message.Should().Be("Occurs Recurring Weekly. Schedule will not be used");
        }
    }
}
