namespace OCRManager
{
    public interface IOcrService
    {
        /// <summary>
        /// Checks if an account number is valid
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        bool IsValidAccountNumber(string accountNumber);

        /// <summary>
        /// Parses and validates an OCR, returning a parsed version of it together with any possible errors
        /// ILL: Any digit was not correctly parsed (noted by '?')
        /// ERR: Checksum is wrong
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ValidateOcr(string input);

        /// <summary>
        /// Parses and validates an OCR, returning a parsed version of it together with any possible errors
        /// AMB: A digit was not correctly parsed (together with possible combinations)
        /// Note that if input is wrong but there is only one possible combination of numbers that satisfies the checksum
        /// with only modifying one single pipe or underscore, no AMB will be shown.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ValidateAndTryToFixOcr(string input);
    }
}