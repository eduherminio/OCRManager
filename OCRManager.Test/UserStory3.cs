using OCRManager.Impl;
using Xunit;

namespace OCRManager.Test
{
    public class UserStory3
    {
        [Theory]
        [InlineData(@"
 _  _  _  _  _  _  _  _     
| || || || || || || ||_   |
|_||_||_||_||_||_||_| _|  |", "000000051")]
        [InlineData(@"
    _  _  _  _  _  _     _ 
|_||_|| || ||_   |  |  | _ 
  | _||_||_||_|  |  |  | _|", "49006771? ILL")]
        [InlineData(@"
    _  _     _  _  _  _  _ 
  | _| _||_| _ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _ ", "1234?678? ILL")]
        public void Tests(string input, string expectedResult)
        {
            IOcrService service = new OcrService();
            Assert.Equal(expectedResult, service.ValidateOcr(input));
        }

        [Theory]
        [InlineData(@"
    _  _  _  _  _  _  _  _ 
|_||_   ||_ | ||_|| || || |
  | _|  | _||_||_||_||_||_|", "457508000")]
        [InlineData(@"
 _  _     _  _        _  _ 
|_ |_ |_| _|  |  ||_||_||_ 
|_||_|  | _|  |  |  | _| _|", "664371495 ERR")]
        public void CustomTests(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new OcrService().ValidateOcr(input));
        }
    }
}