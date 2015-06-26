namespace Labo.Validation.Attributes
{
    using System;
    using System.Collections;

    using Labo.Validation.Validators;

    /// <summary>
    /// The not equal to validation attribute.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class NotEqualToValidationAttribute : Attribute, IValidationAttribute
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEqualToValidationAttribute"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value automatic compare.</param>
        /// <param name="comparer">The comparer.</param>
        public NotEqualToValidationAttribute(object valueToCompare, IEqualityComparer comparer = null)
        {
            m_Validator = new NotEqualToValidator(valueToCompare, comparer);
        }

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <returns>The validator.</returns>
        public IValidator GetValidator()
        {
            return m_Validator;
        }
    }
}