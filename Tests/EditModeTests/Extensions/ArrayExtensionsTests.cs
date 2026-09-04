using NUnit.Framework;

namespace Commons.Tests.EditModeTests.Extensions
{
    [TestFixture]
    public sealed class ArrayExtensionsTests
    {
        [Test]
        public void IsEmpty_ReflectsLength()
        {
            Assert.IsTrue(new int[0].IsEmpty());
            Assert.IsFalse(new[] { 1 }.IsEmpty());
        }

        [Test]
        public void IsNullOrEmpty_IsTrue_ForNull()
        {
            int[] array = null;
            Assert.IsTrue(array.IsNullOrEmpty());
        }

        [Test]
        public void FirstAndLast_ReturnEndElements()
        {
            var array = new[] { 10, 20, 30 };
            Assert.AreEqual(10, array.First());
            Assert.AreEqual(30, array.Last());
        }

        [Test]
        public void FirstOrDefault_ReturnsFallback_ForEmptyArray()
        {
            Assert.AreEqual(-1, new int[0].FirstOrDefault(-1));
        }

        [Test]
        public void TryGetAt_ReturnsTrueAndItem_WhenInRange()
        {
            var array = new[] { 1, 2, 3 };

            var found = array.TryGetAt(1, out var item);

            Assert.IsTrue(found);
            Assert.AreEqual(2, item);
        }

        [Test]
        public void TryGetAt_ReturnsFalse_WhenOutOfRange()
        {
            var array = new[] { 1, 2, 3 };

            Assert.IsFalse(array.TryGetAt(5, out var item));
            Assert.AreEqual(default(int), item);
        }

        [Test]
        public void ContainsAndIndexOf_LocateValue()
        {
            var array = new[] { 5, 6, 7 };

            Assert.IsTrue(array.Contains(6));
            Assert.AreEqual(1, array.IndexOf(6));
            Assert.AreEqual(-1, array.IndexOf(99));
        }

        [Test]
        public void Swap_ExchangesElements()
        {
            var array = new[] { 1, 2, 3 };

            array.Swap(0, 2);

            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, array);
        }

        [Test]
        public void Copy_ProducesIndependentArray()
        {
            var array = new[] { 1, 2, 3 };

            var copy = array.Copy();
            copy[0] = 99;

            Assert.AreEqual(1, array[0]);
            CollectionAssert.AreEqual(new[] { 99, 2, 3 }, copy);
        }

        [Test]
        public void FindAndExists_UseThePredicate()
        {
            var array = new[] { 1, 2, 3, 4 };

            Assert.IsTrue(array.Exists(x => x > 3));
            Assert.AreEqual(2, array.Find(x => x % 2 == 0));
            Assert.AreEqual(2, array.FindIndex(x => x == 3));
        }

        [Test]
        public void TryFind_ReturnsFirstMatchOrFalse()
        {
            var array = new[] { 1, 2, 3, 4 };

            Assert.IsTrue(array.TryFind(x => x % 2 == 0, out var value));
            Assert.AreEqual(2, value);
            Assert.IsFalse(array.TryFind(x => x > 10, out _));
        }

        [Test]
        public void TryFindLast_ReturnsLastMatchOrFalse()
        {
            var array = new[] { 1, 2, 3, 4 };

            Assert.IsTrue(array.TryFindLast(x => x % 2 == 0, out var value));
            Assert.AreEqual(4, value);
            Assert.IsFalse(array.TryFindLast(x => x > 10, out _));
        }
    }
}
