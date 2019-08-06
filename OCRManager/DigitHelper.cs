using System;
using System.Collections.Generic;
using System.Linq;

namespace OCRManager
{
    public static class DigitHelper
    {
        public static readonly int DigitSize = 7;

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

        public static readonly ICollection<string> ValidDigits = new List<string>()
        {
            Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine
        };

        /// <summary>
        /// Returns the digit represented by input, or -1 otherwise
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetDigit(string input)
        {
            return ValidDigits.ToList().FindIndex(digit => digit == input);
        }

        /// <summary>
        /// Returns the digit represented by the input, or -1 otherwise
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDigitString(string input)
        {
            return GetDigit(input).ToString();
        }
    }
}
