using System;
using Xunit;
using FluentAssertions;
using EjericicioFormacion;
using EjericicioFormacion.Config;

namespace Test.Test
{
    public class ScheduleRecurringDialyTest
    {        
        [Theory]
        [InlineData("2020-01-04", "2020-01-01", "2020-02-1",1,true)]
        [InlineData("2020-01-04", "2020-01-01", "2020-02-1",1,false)]
        public void ScheduleRecurringDialy_Return_Correct_Date(string CurrentDate, string StartDate, string EndDate, int DaysBetweenExecutions, bool Enabled)
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse(CurrentDate), DateTime.Parse(StartDate))
            {                
                EndDate = DateTime.Parse(EndDate),
                DaysBetweenExecutions = DaysBetweenExecutions
            };
            var Calculator = new ScheduleRecurringDialy(Data)
            {
                Enabled = Enabled
            };
            Calculator.GetNextExecutionTime(out _).Should().Be(Enabled ? DateTime.Parse("2020-01-04") : null);
        }
        [Fact]
        public void ScheduleRecurringDialy_Null_EndDate_Return_Correct_Date()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-01-01"));
            var Calculator = new ScheduleRecurringDialy(Data);
            Calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("2020-01-04"));
        }
        [Fact]
        public void ScheduleRecurringDialy_StartDate_BiggerThan_CurrentDate_Return_StartDate()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-15"));
            var Calculator = new ScheduleRecurringDialy(Data);
            Calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("2020-01-15"));
        }
        [Fact]
        public void ScheduleRecurringDialy_EndDate_LessThan_CurrentDate_Return_Null()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("2020-01-13"), DateTime.Parse("2020-01-1"))
            {                
                EndDate = DateTime.Parse("2020-01-12")
            };
            var Calculator = new ScheduleRecurringDialy(Data);
            Calculator.GetNextExecutionTime(out _).Should().Be(null);
        }

        [Fact]
        public void ScheduleRecurringDialy_Days_Less_Than_Zero_Returns_Exception()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-15"))
            {
                DaysBetweenExecutions = -1
            };
            var schedule = new ScheduleRecurringDialy(Data);
            FluentActions.Invoking(() => schedule.GetNextExecutionTime(out _)).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringDialy_Hours_Less_Than_Zero_Returns_Exception()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-15"))
            {
                HoursBetweenExecutions = -1
            };
            var schedule = new ScheduleRecurringDialy(Data);
            FluentActions.Invoking(() => schedule.GetNextExecutionTime(out _)).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringDialy_Mins_Less_Than_Zero_Returns_Exception()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-15"))
            {
                MinBetweenExecutions = -1
            };
            var schedule = new ScheduleRecurringDialy(Data);
            FluentActions.Invoking(() => schedule.GetNextExecutionTime(out _)).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringDialy_Secs_Less_Than_Zero_Returns_Exception()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-15"))
            {
                SecBetweenExecutions = -1
            };
            var schedule = new ScheduleRecurringDialy(Data);
            FluentActions.Invoking(() => schedule.GetNextExecutionTime(out _)).Should().ThrowExactly<FormatException>();
        } 
        [Fact]
        public void ScheduleRecurringDialy_Hours_Serie_Return_Correct_Dates()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("01-01-2020"), DateTime.Parse("01-01-2020"))
            {
                EndDate = DateTime.Parse("04-1-2020"),
                DaysBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = TimeSpan.Parse("04:00"),
                EndHour = TimeSpan.Parse("08:00")
            };
            var calculator = new ScheduleRecurringDialy(Data);
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 06:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 08:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 06:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 08:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringDialy_Mins_Serie_Return_Correct_Dates()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("01-01-2020"), DateTime.Parse("01-01-2020"))
            {
                EndDate = DateTime.Parse("04-1-2020"),
                DaysBetweenExecutions = 2,
                MinBetweenExecutions = 30,
                StartHour = TimeSpan.Parse("04:00"),
                EndHour = TimeSpan.Parse("05:00")
            };
            var calculator = new ScheduleRecurringDialy(Data);
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 04:30"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 05:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 04:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 04:30"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 05:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringDialy_Secs_Serie_Return_Correct_Dates()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("01-01-2020"), DateTime.Parse("01-01-2020"))
            {
                EndDate = DateTime.Parse("04-1-2020"),
                DaysBetweenExecutions = 2,
                SecBetweenExecutions = 30,
                StartHour = TimeSpan.Parse("04:00"),
                EndHour = TimeSpan.Parse("04:01")
            };
            var calculator = new ScheduleRecurringDialy(Data);
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 04:00:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 04:00:30"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("01-01-2020 04:01:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 04:00:00"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 04:00:30"));
            calculator.GetNextExecutionTime(out _).Should().Be(DateTime.Parse("03-01-2020 04:01"));
            calculator.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringDialy_Serie_Return_Correct_Messages()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("01-01-2020"), DateTime.Parse("01-01-2020"))
            {
                EndDate = DateTime.Parse("04-1-2020"),
                DaysBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = TimeSpan.Parse("04:00"),
                EndHour = TimeSpan.Parse("08:00")
            };
            var calculator = new ScheduleRecurringDialy(Data);
            string description;
            calculator.GetNextExecutionTime(out description);
            description.Should().Be("Ocurrs every 2 days between 04:00:00 and 08:00:00. Schedule will be used on 01/01/2020 at 04:00 starting on 01/01/2020 00:00");
            calculator.GetNextExecutionTime(out description);
            description.Should().Be("Ocurrs every 2 days between 04:00:00 and 08:00:00. Schedule will be used on 01/01/2020 at 06:00 starting on 01/01/2020 00:00");
            calculator.GetNextExecutionTime(out _);
            calculator.GetNextExecutionTime(out _);
            calculator.GetNextExecutionTime(out description);
            description.Should().Be("Ocurrs every 2 days between 04:00:00 and 08:00:00. Schedule will be used on 03/01/2020 at 06:00 starting on 01/01/2020 00:00");
            calculator.GetNextExecutionTime(out _);
            calculator.GetNextExecutionTime(out _);
            calculator.GetNextExecutionTime(out description);
            description.Should().Be("Occurs Recurring Dialy. Schedule will not be used");
        }
        [Fact]
        public void ScheduleRecurringDialy_CurrentDateTimeMaxVaule_return_correct_exception()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.MaxValue, DateTime.Parse("01-01-2020"))
            {               
                DaysBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = TimeSpan.Parse("04:00"),
                EndHour = TimeSpan.Parse("08:00")
            };
            FluentActions.Invoking(() => new ScheduleRecurringDialy(Data)).Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        [Fact]
        public void ScheduleRecurringWeekly_GoToDateTimeMaxVaule_return_correct_exception()
        {
            var Data = new ScheduleRecurringDialyData(DateTime.Parse("31-12-9999"), DateTime.Parse("01-01-2020"))
            {
                DaysBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = TimeSpan.Parse("04:00"),
                EndHour = TimeSpan.Parse("04:00")
            };
            var calculator = new ScheduleRecurringDialy(Data);
            calculator.GetNextExecutionTime(out _);
            FluentActions.Invoking(() => calculator.GetNextExecutionTime(out _)).Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}
