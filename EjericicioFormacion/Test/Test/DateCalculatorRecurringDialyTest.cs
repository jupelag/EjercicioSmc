using System;
using Xunit;
using FluentAssertions;
using EjericicioFormacion;
using static EjericicioFormacion.InputDateExceptions;

namespace Test.Test
{
    public class DateCalculatorRecurringDialyTest
    {
        [Theory]
        [InlineData("2020-01-04", "2020-01-01", "2020-02-1",1,true)]
        [InlineData("2020-01-04", "2020-01-01", "2020-02-1",1,false)]
        public void DateCalculatorRecurringDialy_Return_Correct_Date(string CurrentDate, string StartDate, string EndDate, int DaysBetweenExecutions, bool Enabled)
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse(CurrentDate), DateTime.Parse(StartDate))
            {                
                EndDate = DateTime.Parse(EndDate),
                DaysBetweenExecutions = DaysBetweenExecutions
            };
            var Calculator = new DateCalculatorRecurringDialy(Data)
            {
                Enabled = Enabled
            };
            Calculator.GetNextExecutionTime().Should().Be(Enabled ? DateTime.Parse(CurrentDate).AddDays(DaysBetweenExecutions) : null);
        }
        [Fact]
        public void DateCalculatorRecurringDialy_Null_EndDate_Return_Correct_Date()
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-01-01"))
            {
                DaysBetweenExecutions = 1
            };
            var Calculator = new DateCalculatorRecurringDialy(Data);
            Calculator.GetNextExecutionTime().Should().Be(DateTime.Parse("2020-01-05"));
        }
        [Fact]
        public void DateCalculatorRecurringDialy_CurrentDate_Plus_Days_BiggerThan_EndDate_Return_Null()
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-01-01"))
            {
                EndDate = DateTime.Parse("2020-01-05"),
                DaysBetweenExecutions = 10
            };
            var Calculator = new DateCalculatorRecurringDialy(Data);
            Calculator.GetNextExecutionTime().Should().Be(null);
        }
        [Fact]
        public void DateCalculatorRecurringDialy_StartDate_BiggerThan_CurrentDate_Plus_Days_Return_Null()
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-15"))
            {
                DaysBetweenExecutions = 3
            };
            var Calculator = new DateCalculatorRecurringDialy(Data);
            Calculator.GetNextExecutionTime().Should().Be(null);
        }
        [Fact]
        public void DateCalculatorRecurringDialy_EndDate_LessThan_CurrentDate_Plus_Days_Return_Null()
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-1"))
            {
                DaysBetweenExecutions = 3,
                EndDate = DateTime.Parse("2020-01-12")
            };
            var Calculator = new DateCalculatorRecurringDialy(Data);
            Calculator.GetNextExecutionTime().Should().Be(null);
        }

        [Fact]
        public void DateCalculatorRecurringDialy_Days_Less_Than_Zero_Returns_Exception()
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-15"))
            {
                DaysBetweenExecutions = -1
            };
            FluentActions.Invoking(() => new DateCalculatorRecurringDialy(Data)).Should().ThrowExactly<DaysException>();
        }
        [Fact]
        public void DateCalculatorRecurringDialy_NextExecutionTime_Null_Return_Correct_Message()
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse("2020-01-10"), DateTime.Parse("2020-01-1"))
            {
                DaysBetweenExecutions = 3,
                EndDate = DateTime.Parse("2020-01-12")
            };
            var Calculator = new DateCalculatorRecurringDialy(Data);
            Calculator.GetDescription().Should().Be("Occurs once. Schedule will not be used");
        }
        [Fact]
        public void DateCalculatorRecurringDialy_1Day_Return_Correct_Message()
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-01-01"))
            {
                DaysBetweenExecutions = 1
            };
            var Calculator = new DateCalculatorRecurringDialy(Data);            
            Calculator.GetDescription().Should().Be("Ocurrs every day. Schedule will be used on 05/01/2020 at 00:00 starting on 01/01/2020 00:00");
        }
        [Fact]
        public void DateCalculatorRecurringDialy_Days_Return_Correct_Message()
        {
            var Data = new DataDateCalculatorRecurring(DateTime.Parse("2020-01-04"), DateTime.Parse("2020-01-01"))
            {
                DaysBetweenExecutions = 2
            };
            var Calculator = new DateCalculatorRecurringDialy(Data);
            Calculator.GetDescription().Should().Be("Ocurrs every 2 days. Schedule will be used on 06/01/2020 at 00:00 starting on 01/01/2020 00:00");
        }
    }
}
