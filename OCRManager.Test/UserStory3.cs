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
            Assert.True(false);
        }
    }
}