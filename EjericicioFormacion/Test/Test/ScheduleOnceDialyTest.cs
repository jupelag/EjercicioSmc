﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjericicioFormacion;
using Xunit;
using FluentAssertions;
using EjericicioFormacion.Config;

namespace Test.Test
{
    public class ScheduleOnceDialyTest
    {
        [Theory]
        [InlineData("2020-01-04", "2020-01-01", "2020-02-1", "2020, 01, 08, 14:00:00", true)]
        [InlineData("2020-01-04", "2020-01-01", "2020-02-1", "2020, 01, 08, 14:00:00", false)]
        public void DateCalculatorOneDialy_Return_Correct_Date(string CurrentDate, string StartDate, string EndDate, string ProgrammedDate, bool Enabled)
        {
            var Data = new ScheduleOnceData(DateTime.Parse(CurrentDate), DateTime.Parse(StartDate))
            {
                EndDate = DateTime.Parse(EndDate),
                ProgrammedTime = DateTime.Parse(ProgrammedDate)
            };
            var Calculator = new ScheduleOnceDialy(Data)
            {
                Enabled = Enabled
            };
            Calculator.GetNextExecutionTime(out _).Should().Be(Enabled ? DateTime.Parse(ProgrammedDate) : null);
        }
        [Fact]
        public void DateCalculatorOneDialy_StartDate_BiggerThan_ProgrammingDate_Return_Null()
        {
            var Data = new ScheduleOnceData(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-10-04"))
            {
                ProgrammedTime = DateTime.Parse("2020-01-08")
            };
            var Calculator = new ScheduleOnceDialy(Data);
            Calculator.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Fact]
        public void DateCalculatorOneDialy_ProgrammingDate_BiggerThan_EndDate_Return_Null()
        {
            var Data = new ScheduleOnceData(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-01-01"))
            {
                ProgrammedTime = DateTime.Parse("2020-01-08"),
                EndDate = DateTime.Parse("2020-01-05")
            };
            var Calculator = new ScheduleOnceDialy(Data);
            Calculator.GetNextExecutionTime(out _).Should().Be(null);
        }
        [Theory]
        [InlineData("2020-01-04", "2020-01-01", "2020-02-1", "2020, 01, 08, 14:00:00", true)]
        [InlineData("2020-01-04", "2020-01-01", "2020-02-1", "2020, 01, 08, 14:00:00", false)]
        public void DateCalculatorOneDialy_Return_Correct_Description(string CurrentDate, string StartDate, string EndDate, string ProgrammedDate, bool Enabled)
        {
            var Data = new ScheduleOnceData(DateTime.Parse(CurrentDate), DateTime.Parse(StartDate))
            {
                EndDate = DateTime.Parse(EndDate),
                ProgrammedTime = DateTime.Parse(ProgrammedDate)
            };
            var Calculator = new ScheduleOnceDialy(Data)
            {
                Enabled = Enabled
            };
            Calculator.GetNextExecutionTime(out string description);
            description
                .Should()
                .Be(Enabled ? @"Ocurrs once. Schedule will be used on 08/01/2020 at 14:00 starting on 01/01/2020 00:00" : "Occurs once. Schedule will not be used");
        }
        [Fact]
        public void DateCalculatorOneDialy_StartDate_BiggerThan_ProgrammingDate_Return_Correct_Description()
        {
            var Data = new ScheduleOnceData(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-10-04"))
            {
                ProgrammedTime = DateTime.Parse("2020-01-08")
            };
            var Calculator = new ScheduleOnceDialy(Data);
            Calculator.GetNextExecutionTime(out string description);
            description.Should().Be("Occurs once. Schedule will not be used");
        }
        [Fact]
        public void DateCalculatorOneDialy_ProgrammingDate_BiggerThan_EndDate_Return_Correct_Description()
        {
            var Data = new ScheduleOnceData(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-01-01"))
            {
                ProgrammedTime = DateTime.Parse("2020-01-08"),
                EndDate = DateTime.Parse("2020-01-05")
            };
            var Calculator = new ScheduleOnceDialy(Data);
            Calculator.GetNextExecutionTime(out string description);
            description.Should().Be("Occurs once. Schedule will not be used");
        }
    }
}