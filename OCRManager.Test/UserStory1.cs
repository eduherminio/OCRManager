﻿using Xunit;

namespace OCRManager.Test
{
    public class UserStory1
    {
        [Theory]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _
| || || || || || || || || |
|_||_||_||_||_||_||_||_||_|", "000000000")]
        [InlineData(@"

  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |", "111111111")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
|_ |_ |_ |_ |_ |_ |_ |_ |_ ", "222222222")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|", "333333333")]
        [InlineData(@"

|_||_||_||_||_||_||_||_||_|
  |  |  |  |  |  |  |  |  |", "444444444")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
 _| _| _| _| _| _| _| _| _|", "555555555")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
|_||_||_||_||_||_||_||_||_|", "666666666")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |", "777777777")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
|_||_||_||_||_||_||_||_||_|", "888888888")]
        [InlineData(@"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
 _| _| _| _| _| _| _| _| _|", "999999999")]
        [InlineData(@"
    _  _     _  _  _  _  _ 
  | _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|", "123456789")]
        public void Tests(string input, string expectedResult)
        {
            Assert.True(false);
        }
    }
}