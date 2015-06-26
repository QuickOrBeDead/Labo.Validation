namespace Labo.Validation.Tests.Utils
{
    using System;

    using Labo.Validation.Utils;

    using NUnit.Framework;

    [TestFixture]
    public class ComparableUtilsFixture
    {
        private class SourceComparable : IComparable
        {
            private readonly int m_Value;

            public SourceComparable(int value)
            {
                m_Value = value;
            }

            public int CompareTo(object obj)
            {
                return m_Value.CompareTo(((SourceComparable)obj).m_Value);
            }
        }

        private class TargetComparable : SourceComparable
        {
            public TargetComparable(int value)
                : base(value)
            {
            }
        }

        [Test, Sequential]
        public void TryCompareTo(
            [Values(1, 0, 5, 0, 0, -1, 1)]
            IComparable sourceValue,
            [Values(3, 1, 5, 0, -1, 0, "2")]
            IComparable targetValue,
            [Values(true, true, true, true, true, true, false)]
            bool canCompare,
            [Values(-1, -1, 0, 0, 1, -1, 0)]
            int expectedResult)
        {
            int result;
            Assert.AreEqual(canCompare, ComparableUtils.TryCompareTo(sourceValue, targetValue, out result));
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TryCompareTo()
        {
            int result;
            Assert.AreEqual(true, ComparableUtils.TryCompareTo(new SourceComparable(1), new TargetComparable(2), out result));
            Assert.AreEqual(-1, result);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TryCompareToMustThrowArgumentNullExceptionWhenSourceValueIsNull()
        {
            int result;
            ComparableUtils.TryCompareTo(null, 1, out result);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TryCompareToMustThrowArgumentNullExceptionWhenTargetValueIsNull()
        {
            int result;
            ComparableUtils.TryCompareTo(1, null, out result);
        }
    }
}
