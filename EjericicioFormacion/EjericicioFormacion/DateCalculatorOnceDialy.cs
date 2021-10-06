using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjercicioFormacion;
using EjericicioFormacion.Resources;

namespace EjericicioFormacion
{
    public class DateCalculatorOnceDialy : DateCalculatorOnce
    {
        public DateCalculatorOnceDialy(DataDateCalculatorOnce InputData) 
            : base(InputData)
        {
        }
        private bool MustBeRun
        {
            get
            {
                return (base.Enabled == false ||
                    base.CurrentDate > base.ProgrammedTime ||
                    base.ProgrammedTime.EstaEnPeriodo(base.StartDate, base.EndDate) == false) == false;
            }
        }
        public override string GetDescription()
        {
            var NextExecutiontime = this.GetNextExecutionTime();
            if (NextExecutiontime != null)
            {
                return string.Format(DateCalculatorOnceDialyResources.Description,
                    NextExecutiontime.Value.ToString("dd/MM/yyyy"), NextExecutiontime.Value.ToString("HH:mm"), base.StartDate.ToString("dd/MM/yyyy HH:mm"));
            }
            return DateCalculatorOnceDialyResources.NullNextExecutionTimDescripcion;
        }

        public override DateTime? GetNextExecutionTime()
        {
            if (this.MustBeRun == false) { return null; }
            return base.ProgrammedTime;
        }        
    }
}
