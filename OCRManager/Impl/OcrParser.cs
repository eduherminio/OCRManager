using System;
using System.Collections.Generic;
using System.Linq;

namespace OCRManager.Impl
{
    public class OcrParser : IOcrParser
    {
        public string UnknownDigit => "?";

        public string ParseInput(string input)
        {
            var parsedStrings = InternalParseInput(input);

            return string.Concat(parsedStrings.Select(DigitHelper.GetDigitString)).Replace(DigitHelper.UnknownDigitString, UnknownDigit);
        }

        public string ExtractIllFormedDigit(string input)
        {
            var parsedStrings = InternalParseInput(input);

            return parsedStrings.Single(str => DigitHelper.GetDigitString(str).Contains(DigitHelper.UnknownDigitString));
        }

        private static IEnumerable<string> InternalParseInput(string input)
        {
            var lines = input.Split(new[] {"\n"}, StringSplitOptions.None).Skip(1).ToList();
            var characters = Enumerable.Range(0, 9).Select(x => new char[7]).ToList();

            for (var i = 0; i < 9; i++)
            {
                characters[i][0] = lines[0].Length > 0 && lines[0][(i * 3) + 1].Equals('_') ? '1': '0';
                characters[i][1] = lines[1][(i * 3)].Equals('|') ? '1': '0';
                characters[i][2] = lines[1][(i * 3) + 1].Equals('_') ? '1': '0';
                characters[i][3] = lines[1][(i * 3) + 2].Equals('|') ? '1': '0';
                characters[i][4] = lines[2][(i * 3)].Equals('|') ? '1': '0';
                characters[i][5] = lines[2][(i * 3) + 1].Equals('_') ? '1': '0';
                characters[i][6] = lines[2][(i * 3) + 2].Equals('|') ? '1': '0';
            }
            return characters.Select(x => new string(x));
        }
    }
}