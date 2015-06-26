namespace Labo.Validation.Attributes
{
    using System;

    using Labo.Validation.Validators;

    /// <summary>
    /// The between validation attribute.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class BetweenValidationAttribute : Attribute, IValidationAttribute
    {
        /// <summary>
        /// The validator
        /// </summary>
        private readonly IValidator m_Validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetweenValidationAttribute"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">The automatic.</param>
        public BetweenValidationAttribute(IComparable @from, IComparable to)
        {
            m_Validator = new BetweenValidator(@from, to);
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
