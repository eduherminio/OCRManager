using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCRManager.Impl
{
    public class OcrParser : IOcrParser
    {
        private const int _lineLength = 27;

        public string ParseInput(string input)
        {
            ICollection<StringBuilder> digits = new List<StringBuilder>(9)
            {
                new StringBuilder(_lineLength), new StringBuilder(_lineLength), new StringBuilder(_lineLength),
                new StringBuilder(_lineLength), new StringBuilder(_lineLength), new StringBuilder(_lineLength),
                new StringBuilder(_lineLength), new StringBuilder(_lineLength), new StringBuilder(_lineLength)
            };

            var inputInLines = input.Replace("\r", string.Empty).Split('\n');

            for (int lineIndex = 1; lineIndex < inputInLines.Length; ++lineIndex)
            {
                if (inputInLines[lineIndex]?.Length == 0)
                {
                    inputInLines[lineIndex] = new string(' ', _lineLength);
                }
            }

            ParseFirstLine(inputInLines.ElementAt(1), digits);

            ParseNonFirstLine(inputInLines.ElementAt(2), digits);
            ParseNonFirstLine(inputInLines.ElementAt(3), digits);

            if (digits.GroupBy(d => d.Length).Single().Key != DigitHelper.DigitSize)
            {
                throw new OcrParsingException($"Error parsing raw input {input}");
            }

            IEnumerable<string> parsedStrings = digits.Select(sb =>
                sb.Replace('_', '1')
                    .Replace('|', '1')
                    .Replace(' ', '0')
                    .ToString());

            return string.Concat(parsedStrings.Select(DigitHelper.GetDigitString));
        }

        private void ParseFirstLine(string firstLine, ICollection<StringBuilder> digits)
        {
            int counter = 0;
            foreach (char ch in firstLine)
            {
                if (counter % 3 != 1)
                {
                    ++counter;
                    counter %= _lineLength;
                    continue;
                }

                counter = ParseCharacter(digits, counter, ch);
            }
        }

        private void ParseNonFirstLine(string line, ICollection<StringBuilder> digits)
        {
            int counter = 0;
            foreach (char ch in line)
            {
                counter = ParseCharacter(digits, counter, ch);
            }
        }

        private static int ParseCharacter(ICollection<StringBuilder> digits, int counter, char ch)
        {
            int digit = counter / 3;
            digits.ElementAt(digit).Append(ch);

            ++counter;
            counter %= _lineLength;
            return counter;
        }
    }
}
