using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Commons.Tests.EditModeTests.Extensions
{
    [TestFixture]
    public sealed class EnumerableExtensionsTests
    {
        [Test]
        public void HasNext_IsTrue_WhenSequenceHasElements()
        {
            IEnumerable<int> sequence = new[] { 1 };
            Assert.IsTrue(sequence.HasNext());
        }

        [Test]
        public void HasNext_IsFalse_ForEmptySequence()
        {
            IEnumerable<int> sequence = Enumerable.Empty<int>();
            Assert.IsFalse(sequence.HasNext());
        }
    }
}
