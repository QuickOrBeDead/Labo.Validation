namespace Labo.Validation.Attributes
{
    using System;
    using System.Text.RegularExpressions;

    using Labo.Validation.Validators;

    /// <summary>
    /// The regex validation attribute.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class RegexValidationAttribute : Attribute, IValidationAttribute
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexValidationAttribute"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="regexOptions">The regex options.</param>
        public RegexValidationAttribute(string expression, RegexOptions regexOptions = RegexOptions.None)
        {
            m_Validator = new RegexValidator(expression, regexOptions);
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