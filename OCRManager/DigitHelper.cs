using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCRManager
{
    public static class DigitHelper
    {
        public static readonly int DigitSize = 7;

        /// <summary>
        /// Result of FindIndex() when no result is found
        /// </summary>
        public static readonly string UnknownDigitString = "-1";

        public static readonly string Zero = "1101111";

        public static readonly string One = "0001001";

        public static readonly string Two = "1011110";

        public static readonly string Three = "1011011";

        public static readonly string Four = "0111001";

        public static readonly string Five = "1110011";

        public static readonly string Six = "1110111";

        public static readonly string Seven = "1001001";

        public static readonly string Eight = "1111111";

        public static readonly string Nine = "1111011";

        static DigitHelper()
        {
            if (ValidDigits.Any(str => str.Length != DigitSize))
            {
                throw new Exception("Error constructing 'Digits' static class");
            }
        }

        public static readonly List<string> ValidDigits = new List<string>()
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
                if (1 == XORStrings(input, candidateDigit).Count(ch => ch == '1'))
                {
                    yield return ValidDigits.FindIndex(d => d == candidateDigit);
                }
            }
        }

        /// <summary>
        /// Returns an enumeration of valid digits that only differ a character from a valid digit
        /// </summary>
        /// <param name="validDigit"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetSimilarDigits(int validDigit)
        {
            string originalDigit = ValidDigits.ElementAt(validDigit);

            return GetSimilarDigits(originalDigit);
        }

        /// <summary>
        /// Applies XOR operation, char by char, to two strings
        /// Only tested with same size strings
        /// With a little help from https://stackoverflow.com/a/5126635/5459321
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        private static string XORStrings(string str1, string str2)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < DigitSize; ++i)
            {
                char ch1 = str1[i];
                char ch2 = str2[i % DigitSize];

                int result = ch1 ^ ch2;
                sb.Append(result.ToString());
            }

            return sb.ToString();
        }
    }
}
