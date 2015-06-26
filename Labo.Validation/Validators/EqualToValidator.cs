namespace Labo.Validation.Validators
{
    using System;
    using System.Collections;

    using Labo.Validation.Utils;

    /// <summary>
    /// The equal to validator class.
    /// </summary>
    public sealed class EqualToValidator : IValidator
    {
        /// <summary>
        /// The value to compare
        /// </summary>
        private readonly object m_ValueToCompare;

        /// <summary>
        /// The comparer
        /// </summary>
        private readonly IEqualityComparer m_Comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualToValidator"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="System.ArgumentNullException">valueToCompare</exception>
        public EqualToValidator(object valueToCompare, IEqualityComparer comparer = null)
        {
            if (valueToCompare == null)
            {
                throw new ArgumentNullException("valueToCompare");
            }

            m_ValueToCompare = valueToCompare;
            m_Comparer = comparer;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public bool IsValid(object value)
        {
            return AreEqual(value, m_ValueToCompare, m_Comparer);
        }

        /// <summary>
        /// Determines whether [are equal] [the specified values].
        /// </summary>
        /// <param name="sourceValue">The source value.</param>
        /// <param name="destinationValue">The destination value.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns><c>true</c> if the specified values are equal otherwise <c>false</c></returns>
        private static bool AreEqual(object sourceValue, object destinationValue, IEqualityComparer comparer = null)
        {
            if (comparer != null)
            {
                return comparer.Equals(sourceValue, destinationValue);
            }

            IComparable sourceComparable = sourceValue as IComparable;
            if (sourceComparable != null)
            {
                IComparable destinationComparable = destinationValue as IComparable;
                if (destinationComparable != null)
                {
                    int compareResult;
                    if (ComparableUtils.TryCompareTo(sourceComparable, destinationComparable, out compareResult))
                    {
                        return compareResult == 0;
                    }
                }
            }

            return sourceValue == destinationValue;
        }
    }
}
