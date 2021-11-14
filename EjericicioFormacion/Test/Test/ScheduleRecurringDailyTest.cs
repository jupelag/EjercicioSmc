using System;
using Xunit;
using FluentAssertions;
using EjercicioFormacion;
using EjercicioFormacion.Config;

namespace Test.Test
{
    public class ScheduleRecurringDailyTest
    {        
        [Fact]
        public void ScheduleRecurringDaily_Enabled_False_Return_Null()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020,01,04), new DateTime(2020,01,01))
            {
                EndDate = new DateTime(2020,02,01),
                DaysBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data))
            {
                Enabled = false
            };
            schedule.GetNextExecutionTime(out _).Should().Be( null);
        }
        [Fact]
        public void ScheduleRecurringDaily_Enabled_Return_Correct_Date()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020, 01, 04), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 02, 01),
                DaysBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020,01,04));
        }
        [Fact]
        public void ScheduleRecurringDaily_Null_EndDate_Return_Correct_Date()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020,01,04), new DateTime(2020,01,01));
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 04));
        }
        [Fact]
        public void ScheduleRecurringDaily_StartDate_BiggerThan_CurrentDate_Return_StartDate()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020,01,10),new DateTime(2020,01,15));
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020,01,15));
        }
        [Fact]
        public void ScheduleRecurringDaily_EndDate_LessThan_CurrentDate_Return_Null()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020,01,13), new DateTime(2020,01,01))
            {                
                EndDate = new DateTime(2020,01,12)
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }

        [Fact]
        public void ScheduleRecurringDaily_Days_Less_Than_Zero_Returns_Exception()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020,01,10),new DateTime(2020,01,15))
            {
                DaysBetweenExecutions = -1
            };            
            FluentActions.Invoking(() => new ScheduleRecurringDaily(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringDaily_Hours_Less_Than_Zero_Returns_Exception()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020, 01, 10), new DateTime(2020, 01, 15))
            {
                HoursBetweenExecutions = -1
            };            
            FluentActions.Invoking(() => new ScheduleRecurringDaily(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringDaily_Mins_Less_Than_Zero_Returns_Exception()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020, 01, 10), new DateTime(2020, 01, 15))
            {
                MinsBetweenExecutions = -1
            };            
            FluentActions.Invoking(() => new ScheduleRecurringDaily(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringDaily_Secs_Less_Than_Zero_Returns_Exception()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020, 01, 10), new DateTime(2020, 01, 15))
            {
                SecsBetweenExecutions = -1
            };            
            FluentActions.Invoking(() => new ScheduleRecurringDaily(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        } 
        [Fact]
        public void ScheduleRecurringDaily_Hours_Serie_Return_Correct_Dates()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020, 01, 01), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020,01,04),
                DaysBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04,00,00),
                EndHour = new TimeSpan(08,00,00)
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 06, 00,00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 08, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringDaily_Mins_Serie_Return_Correct_Dates()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020, 01, 01), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 04),
                DaysBetweenExecutions = 2,
                MinsBetweenExecutions = 30,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(05, 00, 00)
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 04, 30, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 04, 30, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringDaily_Secs_Serie_Return_Correct_Dates()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020, 01, 01), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 04),
                DaysBetweenExecutions = 2,
                SecsBetweenExecutions = 30,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(04, 01, 00)
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 04, 00, 30));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 01, 04, 01, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 04, 00, 30));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 03, 04, 01, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringDaily_Serie_Return_Correct_Messages()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020, 01, 01), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 04),
                DaysBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00)
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            string description;
            schedule.GetNextExecutionTime(out description);
            description.Should().Be("Ocurrs every 2 days between 04:00:00 and 08:00:00. Schedule will be used on 01/01/2020 4:00:00 starting on 01/01/2020 0:00:00");
            schedule.GetNextExecutionTime(out description);
            description.Should().Be("Ocurrs every 2 days between 04:00:00 and 08:00:00. Schedule will be used on 01/01/2020 6:00:00 starting on 01/01/2020 0:00:00");
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out description);
            description.Should().Be("Ocurrs every 2 days between 04:00:00 and 08:00:00. Schedule will be used on 03/01/2020 6:00:00 starting on 01/01/2020 0:00:00");
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out description);
            description.Should().Be("Occurs Recurring Dialy. Schedule will not be used");
        }
        [Fact]
        public void ScheduleRecurringDaily_CurrentDateTimeMaxVaule_return_correct_exception()
        {
            var data = new ScheduleRecurringDailyData(DateTime.MaxValue, new DateTime(2020,01,01))
            {               
                DaysBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00)
            };
            FluentActions.Invoking(() => new ScheduleRecurringDaily(new ScheduleData(data))).Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        [Fact]
        public void ScheduleRecurringDaily_GoToDateTimeMaxVaule_return_correct_exception()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(9999,12,31), new DateTime(2020, 01, 01))
            {
                DaysBetweenExecutions = 2,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(04, 00, 00)
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _);
            FluentActions.Invoking(() => schedule.GetNextExecutionTime(out _)).Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        [Fact]
        public void ScheduleRecurringDaily_CurrentDate_Equals_StarDate_Bigger_endHour_Return_Correct_Date()
        {
            var data = new ScheduleRecurringDailyData(new DateTime(2020,01,01,04,02,00), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 04),
                DaysBetweenExecutions = 2,
                SecsBetweenExecutions = 30,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(04, 01, 00)
            };
            var schedule = new ScheduleRecurringDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 04, 00, 30));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 02, 04, 01, 00));
        }
    }
}
