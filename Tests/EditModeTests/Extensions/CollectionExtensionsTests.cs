using System.Collections.Generic;
using NUnit.Framework;

namespace Commons.Tests.EditModeTests.Extensions
{
    [TestFixture]
    public sealed class CollectionExtensionsTests
    {
        [Test]
        public void IsEmpty_IsTrue_ForEmptyCollection()
        {
            ICollection<int> collection = new List<int>();
            Assert.IsTrue(collection.IsEmpty());
        }

        [Test]
        public void IsEmpty_IsFalse_ForNonEmptyCollection()
        {
            ICollection<int> collection = new List<int> { 1 };
            Assert.IsFalse(collection.IsEmpty());
        }

        [Test]
        public void IsNullOrEmpty_IsTrue_ForNull()
        {
            ICollection<int> collection = null;
            Assert.IsTrue(collection.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_IsFalse_ForNonEmpty()
        {
            ICollection<int> collection = new List<int> { 1 };
            Assert.IsFalse(collection.IsNullOrEmpty());
        }

        [Test]
        public void ContainsAll_IsTrue_WhenAllPresent()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.IsTrue(collection.ContainsAll(1, 2));
        }

        [Test]
        public void ContainsAll_IsFalse_WhenAnyMissing()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.ContainsAll(1, 4));
        }

        [Test]
        public void ContainsAny_IsTrue_WhenAnyPresent()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.IsTrue(collection.ContainsAny(4, 2));
        }

        [Test]
        public void ContainsAny_IsFalse_WhenNonePresent()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.IsFalse(collection.ContainsAny(5, 6));
        }

        [Test]
        public void RemoveAll_RemovesItems_AndReturnsTrueWhenAllRemoved()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };

            var result = collection.RemoveAll(1, 2);

            Assert.IsTrue(result);
            CollectionAssert.AreEquivalent(new[] { 3 }, collection);
        }

        [Test]
        public void RemoveAll_ReturnsFalse_WhenAnItemWasMissing()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };

            var result = collection.RemoveAll(1, 9);

            Assert.IsFalse(result);
            CollectionAssert.AreEquivalent(new[] { 2, 3 }, collection);
        }
    }
}
