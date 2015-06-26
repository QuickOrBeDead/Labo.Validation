namespace Labo.Validation.Attributes
{
    using System;

    using Labo.Validation.Validators;

    /// <summary>
    /// The length validation attribute.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class LengthValidationAttribute : Attribute, IValidationAttribute
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LengthValidationAttribute"/> class.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public LengthValidationAttribute(int min, int max = -1)
        {
            m_Validator = new LengthValidator(min, max);
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