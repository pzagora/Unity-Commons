using NUnit.Framework;

namespace Commons.Tests.EditModeTests.Math
{
    [TestFixture]
    public sealed class HashTests
    {
        [Test]
        public void Compute_IsDeterministic_ForSameInputs()
        {
            Assert.AreEqual(Hash.Compute(1, 2, 3), Hash.Compute(1, 2, 3));
        }

        [Test]
        public void Compute_IsOrderSensitive()
        {
            Assert.AreNotEqual(Hash.Compute(1, 2), Hash.Compute(2, 1));
        }

        [Test]
        public void Compute_WithSingleArgument_EqualsItsHashCode()
        {
            var value = "commons";
            Assert.AreEqual(value.GetHashCode(), Hash.Compute(value));
        }

        [Test]
        public void Compute_WithNoArguments_ReturnsSeed()
        {
            Assert.AreEqual(0, Hash.Compute());
        }
    }
}
