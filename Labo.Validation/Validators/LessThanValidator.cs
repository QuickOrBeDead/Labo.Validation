﻿namespace Labo.Validation.Validators
{
    using System;

    using Labo.Validation.Utils;

    /// <summary>
    /// The less than validator class.
    /// </summary>
    public sealed class LessThanValidator : IValidator
    {
        /// <summary>
        /// The value to compare
        /// </summary>
        private readonly IComparable m_ValueToCompare;

        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanValidator"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <exception cref="System.ArgumentNullException">valueToCompare</exception>
        public LessThanValidator(IComparable valueToCompare)
        {
            if (valueToCompare == null)
            {
                throw new ArgumentNullException("valueToCompare");
            }

            m_ValueToCompare = valueToCompare;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            IComparable comparableValue = value as IComparable;
            if (comparableValue == null)
            {
                return false;
            }

            int compareResult;
            if (ComparableUtils.TryCompareTo(m_ValueToCompare, comparableValue, out compareResult))
            {
                return compareResult > 0;
            }

            return false;
        }
    }
}