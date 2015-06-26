namespace Labo.Validation.Utils
{
    using System;

    /// <summary>
    /// The comparable utils.
    /// </summary>
    public static class ComparableUtils
    {
        /// <summary>
        /// Compares the specified value with from value.
        /// </summary>
        /// <param name="sourceValue">The source value.</param>
        /// <param name="targetValue">The target value.</param>
        /// <param name="result">A value that indicates the relative order of the objects being compared.</param>
        /// <returns><c>true</c> if two values could be compared, otherwise <c>false</c>.</returns>
        public static bool TryCompareTo(IComparable sourceValue, IComparable targetValue, out int result)
        {
            if (sourceValue == null)
            {
                throw new ArgumentNullException("sourceValue");
            }

            if (targetValue == null)
            {
                throw new ArgumentNullException("targetValue");
            }

            if (!sourceValue.GetType().IsInstanceOfType(targetValue))
            {
                result = 0;
                return false;
            }

            result = sourceValue.CompareTo(targetValue);
            return true;
        }
    }
}
