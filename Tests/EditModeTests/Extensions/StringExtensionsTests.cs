using NUnit.Framework;
using Commons.Extensions;

namespace Commons.Tests.EditModeTests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("test", "Test")]
        [TestCase("hello world", "Hello world")]
        [TestCase(null, "")]
        [TestCase("", "")]
        public void Check_IsCorrect_FirstToUpper(string input, string expectedResult)
        {
            var result = input.FirstToUpper();
            
            Assert.NotNull(result);
            Assert.AreEqual(result, expectedResult);
        }
    }
}