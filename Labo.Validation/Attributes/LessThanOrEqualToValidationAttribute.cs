namespace Labo.Validation.Attributes
{
    using System;

    using Labo.Validation.Validators;

    /// <summary>
    /// The less than or equal to validation attribute.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class LessThanOrEqualToValidationAttribute : Attribute, IValidationAttribute
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanOrEqualToValidationAttribute"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value automatic compare.</param>
        public LessThanOrEqualToValidationAttribute(IComparable valueToCompare)
        {
            m_Validator = new LessThanOrEqualToValidator(valueToCompare);
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