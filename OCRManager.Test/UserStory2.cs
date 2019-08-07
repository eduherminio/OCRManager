using OCRManager.Impl;
using Xunit;

namespace OCRManager.Test
{
    public class UserStory2
    {
        [Theory]
        [InlineData("711111111", true)]
        [InlineData("123456789", true)]
        [InlineData("490867715", true)]
        [InlineData("888888888", false)]
        [InlineData("490067715", false)]
        [InlineData("012345678", false)]
        public void Tests(string accountNumber, bool isValid)
        {
            IOcrService service = new OcrService();
            Assert.Equal(isValid, service.IsValidAccountNumber(accountNumber));
        }
    }
}