namespace OCRManager
{
    /// <summary>
    /// Class that parses a pipes and underscores based input into a more legible one.
    /// </summary>
    public interface IOcrParser
    {
        /// <summary>
        /// Returns a string representation ocr digits extracted from input.
        /// UnknownDigit is used where digit parsing is not correct
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ParseInput(string input);

        /// <summary>
        /// Returns the string that wasn't successfully parsed into a valid digit.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ExtractIllFormedDigit(string input);

        /// <summary>
        /// string to represent unknown digits (wrong parsing)
        /// </summary>
        string UnknownDigit { get; }
    }
}
