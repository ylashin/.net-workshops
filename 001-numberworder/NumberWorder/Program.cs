using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberWorderApp
{
   
    public class Program
    {
        public const string UsageMessage = "Please provide input argument.\r\nUsage : NumberWorder.exe 1234";
        public const string UsageMessageMoreThanOneParam = "Please provide a single input argument.\r\nUsage : NumberWorder.exe 1234";
        
        public static IConsole Console;
        public static INumberWorder NumberWorder;
        static Program()
        {
            Console = new ConsoleWriter();
            NumberWorder = new NumberWorder();
        }

        

        public static void Main(string[] args)
        {
            if (args ==null || args.Length == 0)
            {
                Console.WriteLine(UsageMessage);                
            }
            else if (args.Length > 1)
            {
                Console.WriteLine(UsageMessageMoreThanOneParam);
            }
            else
            {
                try
                {
                    var result = NumberWorder.Parse(args[0]);
                    Console.WriteLine(result);
                }
                catch(Exception ex)
                {
                    var message = string.Format("Error processing your input.\r\nTechnical Details : {0}", ex.Message);
                    Console.WriteLine(message);
                }
            }
            
               
        }
    }
}
