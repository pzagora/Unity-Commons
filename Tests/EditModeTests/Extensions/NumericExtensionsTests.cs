using NUnit.Framework;

namespace Commons.Tests.EditModeTests.Extensions
{
    [TestFixture]
    public sealed class NumericExtensionsTests
    {
        private const float DELTA = 1e-5f;

        [Test]
        public void IsZero_DetectsIntegerZero()
        {
            Assert.IsTrue(0.IsZero());
            Assert.IsFalse(5.IsZero());
        }

        [Test]
        public void IsZero_DetectsFloatZero()
        {
            Assert.IsTrue(0f.IsZero());
            Assert.IsFalse(1.5f.IsZero());
        }

        [Test]
        public void NotZero_IsInverseOfIsZero()
        {
            Assert.IsTrue(3.NotZero());
            Assert.IsFalse(0.NotZero());
        }

        [TestCase(-5, ExpectedResult = 0)]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(7, ExpectedResult = 7)]
        public int ClampToPositive_ClampsIntegers(int value) => value.ClampToPositive();

        [Test]
        public void ClampToPositive_ClampsFloats()
        {
            Assert.AreEqual(0f, (-2.5f).ClampToPositive(), DELTA);
            Assert.AreEqual(3.5f, 3.5f.ClampToPositive(), DELTA);
        }

        [Test]
        public void FloorToInt_FloorsTowardsNegativeInfinity()
        {
            Assert.AreEqual(3, 3.7f.FloorToInt());
            Assert.AreEqual(-2, (-1.2f).FloorToInt());
        }

        [Test]
        public void ToPercentageMultiplier_DividesByHundred()
        {
            Assert.AreEqual(0.5f, 50.ToPercentageMultiplier(), DELTA);
        }

        [Test]
        public void ToAdditionalPercentageMultiplier_AddsOne()
        {
            Assert.AreEqual(1.5f, 50.ToAdditionalPercentageMultiplier(), DELTA);
        }
    }
}
