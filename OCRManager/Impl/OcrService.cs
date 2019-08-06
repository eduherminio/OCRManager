using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OCRManager.Impl
{
    public class OcrService : IOcrService
    {
        private readonly IOcrParser _ocrParser;

        public OcrService()
        {
            _ocrParser = new OcrParser();
        }

        public bool IsValidOcr(string parsedOcr)
        {
            ICollection<int> ocr = parsedOcr.Select(ch => int.Parse(ch.ToString())).ToList();

            int sum = 0;
            for (int i = 1; i < 10; ++i)
            {
                sum += i * ocr.ElementAt(ocr.Count - i);
            }

            return sum % 11 == 0;
        }

        public string ValidateOcr(string input)
        {
            string ocr = _ocrParser.ParseInput(input);

            StringBuilder result = new StringBuilder(ocr);

            if (ocr.Contains(_ocrParser.UnknownDigit))
            {
                result.Append(" ILL");
            }
            else if (!IsValidOcr(ocr))
            {
                result.Append(" ERR");
            }

            return result.ToString();
        }

        public string ValidateAndTryToFixOcr(string input)
        {
            string ocr = _ocrParser.ParseInput(input);

            if (!ocr.Contains(_ocrParser.UnknownDigit) && IsValidOcr(ocr))
            {
                return ocr;
            }
            else if (!ocr.Contains(_ocrParser.UnknownDigit))
            {
                return AmmendWellFormedOcr(ocr);
            }
            else
            {
                return AmmendIllFormedOcr(input, ocr);
            }
        }

        private string AmmendWellFormedOcr(string wrongOcr)
        {
            List<string> candidateOcrs = new List<string>();
            List<int> parsedOcr = wrongOcr.Select(ch => int.Parse(ch.ToString())).ToList();

            for (int digitIndex = 0; digitIndex < parsedOcr.Count; ++digitIndex)
            {
                int digitToModify = parsedOcr[digitIndex];

                var alternativeDigits = DigitHelper.GetSimilarDigits(digitToModify);
                foreach (int alternativeDigit in alternativeDigits)
                {
                    parsedOcr[digitIndex] = alternativeDigit;

                    int sum = 0;
                    for (int i = 1; i < 10; ++i)
                    {
                        sum += i * parsedOcr[parsedOcr.Count - i];
                    }

                    if (sum % 11 == 0)
                    {
                        candidateOcrs.Add(string.Join(string.Empty, parsedOcr));
                    }

#pragma warning disable S4143 // Collection elements should not be replaced unconditionally - Intended behavior
                    parsedOcr[digitIndex] = digitToModify;
#pragma warning restore S4143 // Collection elements should not be replaced unconditionally
                }
            }

            switch (candidateOcrs.Count)
            {
                case 0:
                    throw new OcrParsingException("Error in AmmendWrongChecksum, something's wrong");
                case 1:
                    return string.Join(string.Empty, candidateOcrs.First());
                default:
                    StringBuilder result = new StringBuilder(wrongOcr);

                    result.Append(" AMB [");
                    foreach (string candidate in candidateOcrs.OrderBy(str => str))
                    {
                        result.Append($"'{candidate}', ");
                    }
                    result.Append("]");
                    result.Replace(", ]", "]");

                    return result.ToString();
            }
        }

        private string AmmendIllFormedOcr(string input, string wrongOcr)
        {
            int illFormedDigitIndex = wrongOcr.IndexOf(_ocrParser.UnknownDigit);
            const string sampleDigit = "9";
            wrongOcr = wrongOcr.Replace(_ocrParser.UnknownDigit, sampleDigit);
            List<string> candidateOcrs = new List<string>();
            List<int> parsedOcr = wrongOcr.Select(ch => int.Parse(ch.ToString())).ToList();

            string illFormedDigit = _ocrParser.ExtractIllFormedDigit(input);

            var alternativeDigits = DigitHelper.GetSimilarDigits(illFormedDigit);
            foreach (int alternativeDigit in alternativeDigits)
            {
                parsedOcr[illFormedDigitIndex] = alternativeDigit;

                int sum = 0;
                for (int i = 1; i < 10; ++i)
                {
                    sum += i * parsedOcr[parsedOcr.Count - i];
                }

                if (sum % 11 == 0)
                {
                    candidateOcrs.Add(string.Join(string.Empty, parsedOcr));
                }
            }

            if (candidateOcrs.Count != 1)
            {
                throw new OcrParsingException("Error in AmmendIllFormedOcr, something's wrong" +
                    "There should only be one candidate OCR when initial parsing is ill-formed");
            }

            return string.Join(string.Empty, candidateOcrs.First());
        }
    }
}
