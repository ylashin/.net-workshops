using MarsRoversApp.Types;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversApp.Parsers
{
    public class MarsGrammar
    {
        public static readonly Parser<string> Number = Parse.Digit.AtLeastOnce().Text().Token();

        public static readonly Parser<string> Letter = Parse.Letter.Once().Text().Token();

        public static readonly Parser<string> Letters = Parse.Letter.AtLeastOnce().Text().Token();

        public static Parser<Plateau> Plateau =
           from width in Number
           from height in Number
           select new Plateau()
           {
               Width = int.Parse(width),
               Height = int.Parse(height)
           };

        public static Parser<RoverPosition> RoverPosition =
           from x in Number
           from y in Number
           from orientation in Letter
           select new RoverPosition()
           {
               X = int.Parse(x),
               Y = int.Parse(y),
               Orientation = (Orientation)Enum.Parse(typeof(Orientation), orientation)
           };


        public static Parser<MovementScript> MovementScript =
           from commands in Letters
           select new MovementScript()
           {
               Movements = commands.ToArray()
               .Select(a => (MovementAction)Enum.Parse(typeof(MovementAction), a.ToString())).ToList()
           };

        public static Parser<Rover> Rover =
           from p in RoverPosition
           from ms in MovementScript
           select new Rover()
           {
               Position = p,
               MovementScript = ms
           };


        public static Parser<IEnumerable<Rover>> Rovers =
            Rover.Many().Select(rover => rover).End();

        public static Parser<MarsExplorer> MarsExplorer =
           from plateau in Plateau
           from rovers in Rovers
           select new MarsExplorer(plateau, rovers.ToList());
          

        public static MarsExplorer TryParseExplorer(string input)
        {
            try
            {
                return MarsExplorer.Parse(input);
            }
            catch
            {
                return new MarsExplorer(null, null) ;
            }
            
        }
    }
}
