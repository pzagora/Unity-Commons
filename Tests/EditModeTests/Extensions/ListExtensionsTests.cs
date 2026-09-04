using System.Collections.Generic;
using NUnit.Framework;

namespace Commons.Tests.EditModeTests.Extensions
{
    [TestFixture]
    public sealed class ListExtensionsTests
    {
        [Test]
        public void GetCountSafely_ReturnsZero_ForNull()
        {
            List<int> list = null;
            Assert.AreEqual(0, list.GetCountSafely());
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [Test]
        public void NotNullNorEmpty_IsTrue_ForPopulatedList()
        {
            var list = new List<int> { 1 };
            Assert.IsTrue(list.NotNullNorEmpty());
        }

        [Test]
        public void FirstAndLast_ReturnEndElements()
        {
            var list = new List<int> { 10, 20, 30 };
            Assert.AreEqual(10, list.First());
            Assert.AreEqual(30, list.Last());
        }

        [Test]
        public void AddUnique_AddsOnlyWhenAbsent()
        {
            var list = new List<int> { 1, 2 };

            Assert.IsTrue(list.AddUnique(3));
            Assert.IsFalse(list.AddUnique(2));
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list);
        }

        [Test]
        public void InsertFirst_PrependsItem()
        {
            var list = new List<int> { 2, 3 };

            list.InsertFirst(1);

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list);
        }

        [Test]
        public void InsertSorted_KeepsOrderAndReturnsIndex()
        {
            var list = new List<int> { 1, 3, 5 };

            var index = list.InsertSorted(4);

            Assert.AreEqual(2, index);
            CollectionAssert.AreEqual(new[] { 1, 3, 4, 5 }, list);
        }

        [Test]
        public void Swap_ExchangesElements()
        {
            var list = new List<int> { 1, 2, 3 };

            list.Swap(0, 2);

            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, list);
        }

        [Test]
        public void SwapLast_ExchangesWithLastElement()
        {
            var list = new List<int> { 1, 2, 3 };

            list.SwapLast(0);

            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, list);
        }

        [Test]
        public void RemoveFirst_RemovesLeadingElement()
        {
            var list = new List<int> { 1, 2, 3 };

            list.RemoveFirst();

            CollectionAssert.AreEqual(new[] { 2, 3 }, list);
        }

        [Test]
        public void RemoveFirst_WithPredicate_RemovesMatch()
        {
            var list = new List<int> { 1, 2, 3, 4 };

            var removed = list.RemoveFirst(x => x % 2 == 0);

            Assert.IsTrue(removed);
            CollectionAssert.AreEqual(new[] { 1, 3, 4 }, list);
        }

        [Test]
        public void RevokeAt_RemovesAndReturnsElement()
        {
            var list = new List<int> { 1, 2, 3 };

            var revoked = list.RevokeAt(1);

            Assert.AreEqual(2, revoked);
            CollectionAssert.AreEqual(new[] { 1, 3 }, list);
        }

        [Test]
        public void TryGetAt_RespectsBounds()
        {
            var list = new List<int> { 1, 2, 3 };

            Assert.IsTrue(list.TryGetAt(2, out var inRange));
            Assert.AreEqual(3, inRange);
            Assert.IsFalse(list.TryGetAt(3, out _));
        }

        [Test]
        public void TryFind_ReturnsMatch()
        {
            var list = new List<int> { 1, 2, 3 };

            Assert.IsTrue(list.TryFind(x => x == 2, out var value));
            Assert.AreEqual(2, value);
            Assert.IsFalse(list.TryFind(x => x == 99, out _));
        }

        [Test]
        public void TryFindLast_ReturnsLastMatchOrFalse()
        {
            var list = new List<int> { 1, 2, 3, 4 };

            Assert.IsTrue(list.TryFindLast(x => x % 2 == 0, out var value));
            Assert.AreEqual(4, value);
            Assert.IsFalse(list.TryFindLast(x => x > 10, out _));
        }
    }
}
