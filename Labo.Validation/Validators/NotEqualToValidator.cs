namespace Labo.Validation.Validators
{
    using System;
    using System.Collections;

    /// <summary>
    /// The not equal to validator class.
    /// </summary>
    public sealed class NotEqualToValidator : IValidator
    {
        /// <summary>
        /// The equal to validator
        /// </summary>
        private readonly IValidator m_EqualToValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEqualToValidator"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="System.ArgumentNullException">valueToCompare</exception>
        public NotEqualToValidator(object valueToCompare, IEqualityComparer comparer = null)
        {
            if (valueToCompare == null)
            {
                throw new ArgumentNullException("valueToCompare");
            }

            m_EqualToValidator = new EqualToValidator(valueToCompare, comparer);
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public bool IsValid(object value)
        {
            return !m_EqualToValidator.IsValid(value);
        }
    }
}
