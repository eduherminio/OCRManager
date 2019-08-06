using System;
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
            else
            {
                return AmmendOcr(ocr);
            }
        }

        private string AmmendOcr(string ocr)
        {
            throw new NotImplementedException();
        }
    }
}
