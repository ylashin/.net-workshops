using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberWorderApp
{
    public interface INumberWorder
    {
        string Parse(string input);
    }
    public class NumberWorder : INumberWorder
    {
        private Dictionary<int,string> NumberMap { get; set; }
        public const string EmptyInputMessage = "Input is null or empty or whitespace";
        public const string NonNumericInputMessage = "Non numeric characters were found in input";

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
                throw new ArgumentException(EmptyInputMessage);

            Validate(input);

            var mappedValues = input.ToList()
                .Select(a => NumberMap[int.Parse(a.ToString())]);

            return string.Join("", mappedValues);

        }

        private void Validate(string input)
        {
            foreach (var c in input)
            {
                if (char.IsDigit(c) == false)
                    throw new ArgumentException(NonNumericInputMessage);
            }
        }
    }
}
