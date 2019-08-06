using System;
using System.Runtime.Serialization;

namespace OCRManager
{
    /// <summary>
    /// Error while parsing a raw OCR input
    /// </summary>
    [Serializable]
    internal class OcrParsingException : Exception
    {
        public OcrParsingException()
        {
        }

        public OcrParsingException(string message) : base(message)
        {
        }

        public OcrParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OcrParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}