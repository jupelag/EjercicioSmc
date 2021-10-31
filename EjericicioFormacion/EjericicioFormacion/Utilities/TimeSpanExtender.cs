using System;

namespace EjercicioFormacion.Utilities
{ 
    public static class TimeExpandExtender
    {
        /// <summary>
        /// Comprueba que la fecha se encuentre entre dos fechas diferentes.
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="startHour"></param>
        /// <param name="endHour"></param>        
        public static Boolean IsInTime(this TimeSpan hour, TimeSpan? startHour, TimeSpan? endHour)
        {
            return (startHour == null || hour >= startHour.Value) &&
                   (endHour == null || hour <= endHour.Value);
        }
    }
}
