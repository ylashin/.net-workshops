using MarsRoversApp.Parsers;
using MarsRoversApp.Types;
using Sprache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ValidateInput(args) == false)
                return;

            var fileData = File.ReadAllText(args[0]);
            MarsExplorer explorer = MarsGrammar.TryParseExplorer(fileData);

            if (explorer==null)
            {
                Console.WriteLine("Unable to parse input file based on Mars Explorer schema");
                return;
            }

            if (explorer.Validate()==false)
            {
                Console.WriteLine("Invalid plateua or rover specifications");
                return;
            }

            Console.WriteLine("Parsed explorer file, proceeding to calculate navigation");

            explorer.ProcessNavigation();

            EchoOutput(explorer.Rovers);

        }

        private static void EchoOutput(IEnumerable<Rover> rovers)
        {
            Console.WriteLine();
            Console.WriteLine("Navigation Result");
            Console.WriteLine("======================================================");
            rovers.ToList().ForEach(r=> {
                
                Console.WriteLine("{0} {1} {2}",r.Position.X,r.Position.Y,r.Position.Orientation);
            });
        }

        private static bool ValidateInput(string[] args)
        {
            var UsageMessage = "Please provide path to input file.\r\nUsage : MarsRovers.exe input.txt";
            var UsageMessageMoreThanOneParam = "Please provide a single input argument.\r\nUsage : MarsRovers.exe input.txt";
            var FileDoesNotExistMessage = "File {} does not exit";

            if (args == null || args.Length == 0)
            {
                Console.WriteLine(UsageMessage);
                return false;
            }

            if (args.Length > 1)
            {
                Console.WriteLine(UsageMessageMoreThanOneParam);
                return false;
            }

            if (File.Exists(args[0])==false)
            {
                Console.WriteLine(FileDoesNotExistMessage,args[0]);
                return false;
            }
            return true;
        }
    }
}
