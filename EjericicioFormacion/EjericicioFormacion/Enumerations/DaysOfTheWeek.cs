using System;

namespace EjercicioFormacion.Enumerations
{
    [Flags]
    public enum DaysOfTheWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64
    }
}
