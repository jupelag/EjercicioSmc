using EjercicioFormacion.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using EjercicioFormacion;
using EjercicioFormacion.Enumerations;

namespace Test.Test
{
    public class ScheduleRecurringMonthlyTest
    {
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_First_Wednesday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.First,
                ExecutionDays = MonthlyExecutionDays.Wednesday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 12, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 12, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 12, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 12, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 06, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_First_Weekendday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.First,
                ExecutionDays = MonthlyExecutionDays.WeekendDay,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022,01,15,03,00,00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022,01,15,04,00,00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 15, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 15, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 02, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_First_Weekday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.First,
                ExecutionDays = MonthlyExecutionDays.Weekday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));            
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 01, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_First_day()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.First,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 01, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_First_day_exceedEndHour()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 07, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.First,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 01, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_First_day_CurrentHour_LesserThan_StartHour()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 02, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.First,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 01, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_First_day_Hour_Lesser_Than_Start_Hour()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 02, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.First,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 10, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 01, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Second_Wednesday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Second,
                ExecutionDays = MonthlyExecutionDays.Wednesday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 19, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 19, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 19, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 19, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 13, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Second_Weekendday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Second,
                ExecutionDays = MonthlyExecutionDays.WeekendDay,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 16, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 16, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 16, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 16, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 03, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Second_Weekday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Second,
                ExecutionDays = MonthlyExecutionDays.Weekday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 04, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Second_day()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Second,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 11, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 02, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Third_Wednesday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 5, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Third,
                ExecutionDays = MonthlyExecutionDays.Wednesday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 19, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 19, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 19, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 19, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 20, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Third_Weekendday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Third,
                ExecutionDays = MonthlyExecutionDays.WeekendDay,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 22, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 22, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 22, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 22, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 09, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Thrid_Weekday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Third,
                ExecutionDays = MonthlyExecutionDays.Weekday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 12, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 12, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 12, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 12, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 05, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Third_day()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Third,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 1, 12, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 1, 12, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 1, 12, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 1, 12, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 03, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Fourth_Wednesday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Fourth,
                ExecutionDays = MonthlyExecutionDays.Wednesday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 27, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 27, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 27, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 27, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 07, 27, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Fourth_Weekendday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Fourth,
                ExecutionDays = MonthlyExecutionDays.WeekendDay,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 23, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 23, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 23, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 23, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 10, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Fourth_Weekday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Fourth,
                ExecutionDays = MonthlyExecutionDays.Weekday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 13, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 13, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 13, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 13, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 06, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Fourth_day()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Fourth,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 13, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 13, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 13, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 13, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 04, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Last_Wednesday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Wednesday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 26, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 26, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 26, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 26, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 27, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Last_Weekendday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.WeekendDay,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 30, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 30, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 30, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 30, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 30, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Last_Weekday()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Weekday,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 29, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Last_day()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 04, 30, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectCurrentDate_Change_Year()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 12, 31, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 12, 31, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 12, 31, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 12, 31, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 12, 31, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2023, 03, 31, 03, 00, 00));
        }
        [Fact]
        public void ScheduleRecurringMonthly_CorrectDescription()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 12, 31, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out string description).Should().Be(new DateTime(2022, 12, 31, 03, 00, 00));
            description.Should().Be("Occurs the Last Day of every 3 months every 1 hours, 0 minutes, 0 seconds between 03:00:00 and 06:00:00 starting on 01/01/2022 0:00:00");
        }
        [Fact]
        public void ScheduleRecurringMonthly_ExecutionDays_Not_Configurated_Return_Correct_Exception()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2020, 01, 01, 10, 00, 00), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 01, 03),
                MonthsBetweenExecutions = 2,
                SecsBetweenExecutions = 7200,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),
            };
            FluentActions.Invoking(() => new ScheduleRecurringMonthly(new ScheduleData(data))).Should().ThrowExactly<ArgumentException>();
        }
        [Fact]
        public void ScheduleRecurringMonthly_MonthsBetweenExecutions_Not_Configurated()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 12, 31, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,                
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1
            };
            FluentActions.Invoking(() => new ScheduleRecurringMonthly(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringMonthly_MonthsBetweenExecutions_Zero_Return_Correct_Exception()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 12, 31, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1,
                MonthsBetweenExecutions = 0
            };
            FluentActions.Invoking(() => new ScheduleRecurringMonthly(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringMonthly_TimeSpanBetweenExecutions_Not_Configurated_Return_Correct_Exception()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 12, 31, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),                
                MonthsBetweenExecutions = 2
            };            
            FluentActions.Invoking(() => new ScheduleRecurringMonthly(new ScheduleData(data))).Should().ThrowExactly<FormatException>();
        }
        [Fact]
        public void ScheduleRecurringMonthly_Enabled_False_Return_null_and_Correct_Message()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2020, 01, 01, 10, 00, 00), new DateTime(2020, 01, 01))
            {
                ExecutionDays = MonthlyExecutionDays.Friday,
                EndDate = new DateTime(2020, 01, 03),
                MonthsBetweenExecutions = 2,
                SecsBetweenExecutions = 7200,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(08, 00, 00),                
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.Enabled = false;
            schedule.GetNextExecutionTime(out string message).Should().Be(null);
            message.Should().Be("Occurs Recurring Monthly. Schedule will not be used");
        }
        [Fact]
        public void ScheduleRecurringMonthly_GoToDateTimeMaxVaule_return_correct_exception()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(9999, 12, 31), new DateTime(2020, 01, 01))
            {
                ExecutionDays = MonthlyExecutionDays.Friday,
                HoursBetweenExecutions = 2,
                StartHour = new TimeSpan(04, 00, 00),
                EndHour = new TimeSpan(04, 00, 00),
                MonthsBetweenExecutions = 2,
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _);
            FluentActions.Invoking(() => schedule.GetNextExecutionTime(out _)).Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        [Fact]
        public void ScheduleRecurringMonthly_NullData_Return_Correct_Exception()
        {
            FluentActions.Invoking(() => new ScheduleRecurringMonthly(null)).Should().ThrowExactly<ArgumentNullException>();
        }
        [Fact]
        public void ScheduleRecurringMonthly_EndDate_Works()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {                
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1,
                EndDate = new DateTime(2022,02,01)
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 03, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 04, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 05, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2022, 01, 31, 06, 00, 00));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleRecurringMonthly_EndDate_Works_Correct_Description()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {                
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1,
                EndDate = new DateTime(2022, 02, 01)
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out _);
            schedule.GetNextExecutionTime(out string description).Should().Be(null);
            description.Should().Be("Occurs Recurring Monthly. Schedule will not be used");
        }
        [Fact]
        public void ScheduleRecurringMonthly_CurrentDate_Bigger_Than_EndDate_Return_Null()
        {
            var data = new ScheduleRecurringMonthlyData(new DateTime(2022, 01, 10, 03, 00, 00), new DateTime(2022, 01, 01))
            {
                OrdinalDay = Ordinals.Last,
                ExecutionDays = MonthlyExecutionDays.Day,
                MonthsBetweenExecutions = 3,
                StartHour = new TimeSpan(03, 00, 00),
                EndHour = new TimeSpan(06, 00, 00),
                HoursBetweenExecutions = 1,
                EndDate = new DateTime(2020, 02, 01)
            };
            var schedule = new ScheduleRecurringMonthly(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
    }
}
