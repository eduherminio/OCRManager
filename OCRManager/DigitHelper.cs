using System.Collections.Generic;
using System.Linq;

namespace OCRManager
{
    public static class DigitHelper
    {
        /// <summary>
        /// Result of FindIndex() when no result is found
        /// </summary>
        public const string UnknownDigitString = "-1";

        private const string Zero = "1101111";
        private const string One = "0001001";
        private const string Two = "1011110";
        private const string Three = "1011011";
        private const string Four = "0111001";
        private const string Five = "1110011";
        private const string Six = "1110111";
        private const string Seven = "1001001";
        private const string Eight = "1111111";
        private const string Nine = "1111011";

        private static readonly List<string> ValidDigits = new List<string>()
        {
            Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine
        };

        /// <summary>
        /// Returns the digit represented by the input, or UnknownDigit otherwise
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDigitString(string input)
        {
            return ValidDigits.FindIndex(digit => digit == input).ToString();
        }

        /// <summary>
        /// Returns an enumeration of valid digits that only differ a character from a given parsed input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetSimilarDigits(string input)
        {
            foreach (var candidateDigit in ValidDigits)
            {
                if (input.Where((inputChar, index) => inputChar != candidateDigit[index]).Count() == 1)
                    yield return ValidDigits.FindIndex(d => d == candidateDigit);
            }
        }

        /// <summary>
        /// Returns an enumeration of valid digits that only differ a character from a valid digit
        /// </summary>
        /// <param name="validDigit"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetSimilarDigits(int validDigit)
        {
            var originalDigit = ValidDigits.ElementAt(validDigit);

            return GetSimilarDigits(originalDigit);
        }
    }
}