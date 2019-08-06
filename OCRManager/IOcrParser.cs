namespace OCRManager
{
    public interface IOcrParser
    {
        /// <summary>
        /// Returns a string representation ocr digits extracted from input.
        /// '?' is used where digit parsing is not correct
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ParseInput(string input);
    }
}
