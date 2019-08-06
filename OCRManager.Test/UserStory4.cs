using OCRManager.Impl;
using Xunit;

namespace OCRManager.Test
{
    public class UserStory4
    {
        [Theory]
        [InlineData(@"

  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |", "711111111")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |", "777777177")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
 _|| || || || || || || || |
|_ |_||_||_||_||_||_||_||_|", "200800000")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|", "333393333")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
|_||_||_||_||_||_||_||_||_|", "888888888 AMB ['888886888', '888888880', '888888988']")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
 _| _| _| _| _| _| _| _| _|", "555555555 AMB ['555655555', '559555555']")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
|_||_||_||_||_||_||_||_||_|", "666666666 AMB ['666566666', '686666666']")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
 _| _| _| _| _| _| _| _| _|", "999999999 AMB ['899999999', '993999999', '999959999']")]
        [InlineData(@"
    _  _  _  _  _  _     _ 
|_||_|| || ||_   |  |  ||_ 
  | _||_||_||_|  |  |  | _|", "490067715 AMB ['490067115', '490067719', '490867715']")]
        [InlineData(@"
    _  _     _  _  _  _  _ 
 _| _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|", "123456789")]
        [InlineData(@"
 _     _  _  _  _  _  _    
| || || || || || || ||_   |
|_||_||_||_||_||_||_| _|  |", "000000051")]
        [InlineData(@"
    _  _  _  _  _  _     _ 
|_||_|| ||_||_   |  |  | _ 
  | _||_||_||_|  |  |  | _|", "490867715")]
        public void Tests(string input, string expectedResult)
        {
            IOcrService service = new OcrService();
            Assert.Equal(expectedResult, service.ValidateAndTryToFixOcr(input));
        }

        [Theory]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
| || || || || || || || || |
|_||_||_||_||_||_||_||_||_|", "000000000")]
        [InlineData(@"
    _  _     _  _  _  _  _ 
  | _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|", "123456789")]
        public void CustomTests(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new OcrService().ValidateAndTryToFixOcr(input));
        }
    }
}