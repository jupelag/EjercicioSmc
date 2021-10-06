using System;

namespace EjercicioFormacion
{
    /// <summary>
    /// Contiene métodos extensores para las fechas
    /// </summary>
    public static class DateTimeExtender
    {
        /// <summary>
        /// Comprueba que la fecha se encuentre entre dos fechas diferentes.
        /// </summary>
        /// <param name="LaFecha"></param>
        /// <param name="LaFechaInicial"> Fecha anterior a la que se desea comparar. </param>
        /// <param name="LaFechaFinal"> Fecha posterior a la que se desea comparar. </param>
        /// <returns> Devuelve true si la fecha se encuentra entre las dos fechas pasadas por parametro. </returns>
        public static Boolean EstaEnPeriodo(this DateTime LaFecha, DateTime? LaFechaInicial, DateTime? LaFechaFinal)
        {
            return (LaFechaInicial == null || LaFecha >= LaFechaInicial.Value) &&
                   (LaFechaFinal == null || LaFecha <= LaFechaFinal.Value);
        }
        /// <summary>
        /// Metodo estatico que nos permita comprobar que un periodo es valido.
        /// </summary>
        public static Boolean EsPeriodoValido(DateTime? LaFechaInicial, DateTime? LaFechaFinal)
        {
            return (LaFechaFinal == null || LaFechaInicial == null || LaFechaInicial <= LaFechaFinal);
        }
    }
}

