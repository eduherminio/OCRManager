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

        public bool IsValidAccountNumber(string parsedOcr) => 
            IsValidAccountNumber(parsedOcr.Select(x => int.Parse(x.ToString())));

        public string ValidateOcr(string input)
        {
            var ocr = _ocrParser.ParseInput(input);

            var result = new StringBuilder(ocr);

            if (ocr.Contains(_ocrParser.UnknownDigit))
            {
                result.Append(" ILL");
            }
            else if (!IsValidAccountNumber(ocr))
            {
                result.Append(" ERR");
            }

            return result.ToString();
        }

        public string ValidateAndTryToFixOcr(string input)
        {
            var ocr = _ocrParser.ParseInput(input);

            if (!ocr.Contains(_ocrParser.UnknownDigit) && IsValidAccountNumber(ocr))
            {
                return ocr;
            }

            return !ocr.Contains(_ocrParser.UnknownDigit) 
                ? AmendWellFormedOcr(ocr) 
                : AmendIllFormedOcr(input, ocr);
        }

        private static bool IsValidAccountNumber(IEnumerable<int> accountNumber) =>
            accountNumber
                .Reverse()
                .Select((x, i) => (i + 1) * x)
                .Sum() % 11 == 0;

        private static string AmendWellFormedOcr(string wrongOcr)
        {
            var candidateAccountNumbers = new List<string>();
            var accountNumber = wrongOcr.Select(ch => int.Parse(ch.ToString())).ToList();

            for (int digitIndex = 0; digitIndex < accountNumber.Count; ++digitIndex)
            {
                int digitToModify = accountNumber[digitIndex];

                var alternativeDigits = DigitHelper.GetSimilarDigits(digitToModify);
                foreach (int alternativeDigit in alternativeDigits)
                {
                    accountNumber[digitIndex] = alternativeDigit;

                    if (IsValidAccountNumber(accountNumber))
                    {
                        candidateAccountNumbers.Add(string.Join(string.Empty, accountNumber));
                    }

#pragma warning disable S4143 // Collection elements should not be replaced unconditionally - Intended behavior
                    accountNumber[digitIndex] = digitToModify;
#pragma warning restore S4143 // Collection elements should not be replaced unconditionally
                }
            }

            switch (candidateAccountNumbers.Count)
            {
                case 0:
                    throw new OcrParsingException("Error in AmmendWrongChecksum, something's wrong");
                case 1:
                    return string.Join(string.Empty, candidateAccountNumbers.First());
                default:
                    return $"{wrongOcr} AMB ['{string.Join("', '",  candidateAccountNumbers.OrderBy(str => str))}']";
            }
        }

        private string AmendIllFormedOcr(string input, string wrongOcr)
        {
            int illFormedDigitIndex = wrongOcr.IndexOf(_ocrParser.UnknownDigit);
            wrongOcr = wrongOcr.Replace(_ocrParser.UnknownDigit, "0");
            var accountNumber = wrongOcr.Select(ch => int.Parse(ch.ToString())).ToList();
            var illFormedDigit = _ocrParser.ExtractIllFormedDigit(input);
            var alternativeDigits = DigitHelper.GetSimilarDigits(illFormedDigit);

            var candidateOcrs = new List<string>();

            foreach (int alternativeDigit in alternativeDigits)
            {
                accountNumber[illFormedDigitIndex] = alternativeDigit;

                if (IsValidAccountNumber(accountNumber))
                {
                    candidateOcrs.Add(string.Join(string.Empty, accountNumber));
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
