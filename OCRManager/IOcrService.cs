namespace OCRManager
{
    public interface IOcrService
    {
        /// <summary>
        /// Checks if an OCR is valid
        /// </summary>
        /// <param name="parsedOcr"></param>
        /// <returns></returns>
        bool IsValidOcr(string parsedOcr);

        /// <summary>
        /// Parses and validates an OCR, returning a parsed version of it together with any possible errors
        /// ILL: Any digit was not correctly parsed (noted by '?')
        /// ERR: Checksum is wrong
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ValidateOcr(string input);
    }
}
