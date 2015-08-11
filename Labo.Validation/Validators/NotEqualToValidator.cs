namespace Labo.Validation.Validators
{
    using System;
    using System.Collections;

    /// <summary>
    /// The not equal to validator class.
    /// </summary>
    public sealed class NotEqualToValidator : ValidatorBase
    {
        /// <summary>
        /// The equal to validator
        /// </summary>
        private readonly EqualToValidator m_EqualToValidator;

        /// <summary>
        /// Gets the value to compare.
        /// </summary>
        /// <value>
        /// The value to compare.
        /// </value>
        public object ValueToCompare
        {
            get
            {
                return m_EqualToValidator.ValueToCompare;
            }
        }

        /// <summary>
        /// Gets the comparer.
        /// </summary>
        /// <value>
        /// The comparer.
        /// </value>
        public IEqualityComparer Comparer
        {
            get
            {
                return m_EqualToValidator.Comparer;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEqualToValidator"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="System.ArgumentNullException">valueToCompare</exception>
        public NotEqualToValidator(object valueToCompare, IEqualityComparer comparer = null)
            : base(Constants.ValidationMessageResourceNames.NOT_EQUAL_TO_VALIDATION_MESSAGE)
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
        public override bool IsValid(object value)
        {
            return !m_EqualToValidator.IsValid(value);
        }

        /// <summary>
        /// Sets the validation message parameters.
        /// </summary>
        /// <param name="validationMessageBuilderParameterSetter">The validation message builder parameter setter.</param>
        protected override void SetValidationMessageParameters(Message.IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter)
        {
        }
    }
}
