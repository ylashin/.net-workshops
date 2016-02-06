using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberWorderApp
{
    public class NumberWorder
    {
        private Dictionary<int,string> NumberMap { get; set; }
        public NumberWorder()
        {
            NumberMap = new Dictionary<int, string>()
                {
                    {0,"ZERO"}, {1,"ONE"}, {2,"TWO"}, {3,"THREE"}, {4,"FOUR"},
                    {5,"FIVE"}, {6,"SIX"}, {7,"SEVEN"}, {8,"EIGHT"}, {9,"NINE"}
                };
        }
        public string Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input is null or empty or whitespace");

            StringBuilder builder = new StringBuilder();
            
            foreach(var c in input)
            {
                if (char.IsDigit(c) == false)
                    throw new ArgumentException("Non numeric characters were found in input");

                builder.Append(NumberMap[int.Parse(c.ToString())]);
            }

            return builder.ToString();
            
        }
        
    }
}
