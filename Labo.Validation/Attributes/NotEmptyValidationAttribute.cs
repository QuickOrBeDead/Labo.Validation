namespace Labo.Validation.Attributes
{
    using System;

    using Labo.Validation.Validators;

    /// <summary>
    /// The not empty validation attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class NotEmptyValidationAttribute : Attribute, IValidationAttribute
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEmptyValidationAttribute"/> class.
        /// </summary>
        public NotEmptyValidationAttribute()
        {
            m_Validator = new NotEmptyValidator();
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