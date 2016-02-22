using MarsRoversApp.Parsers;
using MarsRoversApp.Types;
using Sprache; // Love it
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

            var parseResult = GetParser(args[0]);
           
            if (!parseResult.IsSuccess)
            {
                Console.WriteLine(parseResult.Message);
                return;
            }

            var explorer = parseResult.Explorer;
            // Seperate UI interaction from logic
            Console.WriteLine("Parsed explorer file, proceeding to calculate navigation");

            explorer.ProcessNavigation();

            EchoOutput(explorer.Rovers);

        }

        private static ParsePreparationResult GetParser(string filePath)
        {
            var fileData = File.ReadAllText(filePath);
            MarsExplorer explorer = MarsGrammar.TryParseExplorer(fileData);

            if (!explorer.IsParsed) // Avoid using null to indicate failure
            {
                return ParsePreparationResult.CreatedFailedResult("Unable to parse input file based on Mars Explorer schema");                
            }

            if (!explorer.Validate()) // Why not use !
            {
                return ParsePreparationResult.CreatedFailedResult("Invalid plateua or rover specifications");
                
            }

            return  ParsePreparationResult.CreateValidResult(explorer);
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
            const string UsageMessage = "Please provide path to input file.\r\nUsage : MarsRovers.exe input.txt";
            const string UsageMessageMoreThanOneParam = "Please provide a single input argument.\r\nUsage : MarsRovers.exe input.txt";
            const string FileDoesNotExistMessage = "File {0} does not exit";

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
    public class ParsePreparationResult
    {
        public MarsExplorer Explorer { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        private ParsePreparationResult()
        {

        }
        internal static ParsePreparationResult CreatedFailedResult(string msg)
        {
            return new ParsePreparationResult()
            {
                IsSuccess = false,
                Message = msg

            };
        }

        internal static ParsePreparationResult CreateValidResult(MarsExplorer explorer)
        {
            return new ParsePreparationResult()
            {
                IsSuccess = true,
                Explorer = explorer
            };
        }
    }
    
}
