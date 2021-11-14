using System;
using EjercicioFormacion;
using Xunit;
using FluentAssertions;
using EjercicioFormacion.Config;

namespace Test.Test
{
    public class ScheduleOnceDailyTest
    {
        [Fact]
        public void ScheduleOnceDialy_Enabled_False_Return_Null()
        {
            var data = new ScheduleOnceData(new DateTime(2020,01,04), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020,02,01),
                ProgrammedTime = new DateTime(2020,01,08,14,00,00)
            };
            var schedule = new ScheduleOnceDaily(new ScheduleData(data))
            {
                Enabled = false
            };
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleOnceDialy_Enabled_Return_Correct_Date()
        {
            var data = new ScheduleOnceData(new DateTime(2020, 01, 04), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 02, 01),
                ProgrammedTime = new DateTime(2020, 01, 08, 14, 00, 00)
            };
            var schedule = new ScheduleOnceDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(new DateTime(2020, 01, 08, 14, 00, 00));
        }
        [Fact]
        public void ScheduleOnceDialy_StartDate_BiggerThan_ProgrammingDate_Return_Null()
        {
            var data = new ScheduleOnceData(new DateTime(2020,01,04),new DateTime(2020,10,04))
            {
                ProgrammedTime = new DateTime(2020,01,08)
            };
            var schedule = new ScheduleOnceDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleOnceDialy_ProgrammingDate_BiggerThan_EndDate_Return_Null()
        {
            var data = new ScheduleOnceData(new DateTime(2020, 01, 04), new DateTime(2020, 10, 01))
            {
                ProgrammedTime = new DateTime(2020,01,08),
                EndDate = new DateTime(2020,01,05)
            };
            var schedule = new ScheduleOnceDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void ScheduleOnceDialy_Enabled_False_Return_Correct_Message()
        {
            var data = new ScheduleOnceData(new DateTime(2020, 01, 04), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 02, 01),
                ProgrammedTime = new DateTime(2020, 01, 08, 14, 00, 00)
            };
            var schedule = new ScheduleOnceDaily(new ScheduleData(data))
            {
                Enabled = false
            };
            schedule.GetNextExecutionTime(out string description);
            description
                .Should()
                .Be("Occurs once. Schedule will not be used");
        }
        [Fact]
        public void ScheduleOnceDialy_Enabled_Return_Correct_Correct_Message()
        {
            var data = new ScheduleOnceData(new DateTime(2020, 01, 04), new DateTime(2020, 01, 01))
            {
                EndDate = new DateTime(2020, 02, 01),
                ProgrammedTime = new DateTime(2020, 01, 08, 14, 00, 00)
            };
            var schedule = new ScheduleOnceDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out string description);
            description
                .Should()
                .Be(@"Ocurrs once. Schedule will be used on 08/01/2020 14:00:00 starting on 01/01/2020 0:00:00");
        }
        [Fact]
        public void OnceDialy_StartDate_BiggerThan_ProgrammingDate_Return_Correct_Description()
        {
            var data = new ScheduleOnceData(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-10-04"))
            {
                ProgrammedTime = new DateTime(2020,01,08)
            };
            var schedule = new ScheduleOnceDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out string description);
            description.Should().Be("Occurs once. Schedule will not be used");
        }
        [Fact]
        public void OnceDialy_ProgrammingDate_BiggerThan_EndDate_Return_Correct_Description()
        {
            var data = new ScheduleOnceData(new DateTime(2020,01,04), new DateTime(2020,01,01))
            {
                ProgrammedTime = new DateTime(2020,01,08),
                EndDate = new DateTime(2020,01,05)
            };
            var schedule = new ScheduleOnceDaily(new ScheduleData(data));
            schedule.GetNextExecutionTime(out string description);
            description.Should().Be("Occurs once. Schedule will not be used");
        }
    }
}
