using System.Collections.Generic;
using System.Linq;

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
    }
}
