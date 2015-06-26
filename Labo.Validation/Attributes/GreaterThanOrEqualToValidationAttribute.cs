namespace Labo.Validation.Attributes
{
    using System;

    using Labo.Validation.Validators;

    /// <summary>
    /// The greater than or equal to validation attribute.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class GreaterThanOrEqualToValidationAttribute : Attribute, IValidationAttribute
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanOrEqualToValidationAttribute"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value automatic compare.</param>
        public GreaterThanOrEqualToValidationAttribute(IComparable valueToCompare)
        {
            m_Validator = new GreaterThanOrEqualToValidator(valueToCompare);
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