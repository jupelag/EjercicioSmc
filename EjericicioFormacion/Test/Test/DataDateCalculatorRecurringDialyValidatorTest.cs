using System;
using Xunit;
using FluentAssertions;
using EjericicioFormacion;
using static EjericicioFormacion.InputDateExceptions;

namespace Test.Test
{
    public class DataDateCalculatorRecurringDialyValidatorTest
    {
        [Fact]
        public void DataDateCalculatorRecurringDialyValidatorTest_Days_Less_Than_Zero_Returns_Exception()
        {
            FluentActions.Invoking(()=> DataDateCalculatorRecurringDialyValidator.ValidateDays(
                new DataDateCalculatorRecurring(DateTime.Parse("2020-01-01"), DateTime.Parse("2020-01-01")) 
                {
                    DaysBetweenExecutions = -1,
                })).Should().ThrowExactly<DaysException>();
        }
    }
}
