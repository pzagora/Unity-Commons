using Commons.Extensions;
using NUnit.Framework;

namespace Commons.Tests.EditModeTests.Extensions
{
    [TestFixture(typeof(string))]
    [TestFixture(typeof(object))]
    public class GenericExtensionsTests<T> where T : class
    {
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = true)]
        public bool Check_IsCorrect_IsNull(T input)
        {
            var result = input.IsNull();
            return result;
        }
        
        [TestCase("", ExpectedResult = true)]
        [TestCase(null, ExpectedResult = false)]
        public bool Check_IsCorrect_NotNull(T input)
        {
            var result = input.NotNull();
            return result;
        }
    }
}
