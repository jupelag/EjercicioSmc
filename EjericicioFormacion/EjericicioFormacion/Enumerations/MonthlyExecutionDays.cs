using System;

namespace EjercicioFormacion.Enumerations
{
    [Flags]
    public enum MonthlyExecutionDays
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
        Day = Monday | Tuesday | Wednesday | Thursday | Friday | Saturday | Sunday,
        Weekday = Monday | Tuesday | Wednesday | Thursday | Friday,
        WeekendDay = Saturday | Sunday,
    }
}
