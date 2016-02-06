using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberWorderApp
{
   
    public class Program
    {
        public static IConsole Console;
        public static INumberWorder NumberWorder;
        static Program()
        {
            Console = new ConsoleWriter();
            NumberWorder = new NumberWorder();
        }

        public static void Main(string[] args)
        {

            
        }
    }
}
