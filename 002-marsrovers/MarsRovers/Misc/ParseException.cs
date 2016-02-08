using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp
{
    public class ParseException : ApplicationException
    {
        public ParseException(string message)
        : base(message)
        {
        }
    }
}
