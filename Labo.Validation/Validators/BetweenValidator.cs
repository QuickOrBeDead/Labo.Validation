namespace Labo.Validation.Validators
{
    using System;
    using System.Globalization;

    using Labo.Validation.Utils;

    /// <summary>
    /// The between validator class.
    /// </summary>
    public sealed class BetweenValidator : IBetweenValidator
    {
        /// <summary>
        /// The from value.
        /// </summary>
        private readonly IComparable m_From;

        /// <summary>
        /// The to value.
        /// </summary>
        private readonly IComparable m_To;

        /// <summary>
        /// Gets from value.
        /// </summary>
        /// <value>
        /// The from value.
        /// </value>
        public IComparable FromValue
        {
            get
            {
                return m_From;
            }
        }

        /// <summary>
        /// Gets the to value.
        /// </summary>
        /// <value>
        /// The to value.
        /// </value>
        public IComparable ToValue
        {
            get
            {
                return m_To;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BetweenValidator"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">The automatic.</param>
        public BetweenValidator(IComparable @from, IComparable to)
        {
            if (@from == null)
            {
                throw new ArgumentNullException("from");
            }

            if (to == null)
            {
                throw new ArgumentNullException("to");
            }

            if (!to.GetType().IsInstanceOfType(@from))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "'To' value should be type of '{0}'", @from.GetType().FullName));
            }

            if (to.CompareTo(@from) == -1)
            {
                throw new ArgumentOutOfRangeException("to", "To should be larger than from.");
            }

            m_From = @from;
            m_To = to;
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

            int compareWithFromValueResult, compareWithToValueResult;
            if (ComparableUtils.TryCompareTo(comparableValue, FromValue, out compareWithFromValueResult) &&
                ComparableUtils.TryCompareTo(comparableValue, ToValue, out compareWithToValueResult))
            {
                return compareWithFromValueResult >= 0 && compareWithToValueResult <= 0;
            }

            return false;
        }
    }
}
