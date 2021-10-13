using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjericicioFormacion
{
    public class InputDateExceptions
    {
        public class DaysException : Exception
        {
            public DaysException(string message) : base(message)
            {
            }
        }
    }
}
