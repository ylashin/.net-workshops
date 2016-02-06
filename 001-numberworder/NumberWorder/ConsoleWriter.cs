using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberWorderApp
{
    public interface IConsole
    {
        void WriteLine(string value);
    }
    public class ConsoleWriter : IConsole
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
