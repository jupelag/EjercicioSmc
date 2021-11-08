using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using EjercicioFormacion;

namespace Test.Test
{
    public class ScheduleTest
    {
        [Fact]
        public void ScheduleRecurringWeekly_Null_Data_Return_Correct_Exception()
        {
            FluentActions.Invoking(() => new ScheduleRecurringWeekly(null)).Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
