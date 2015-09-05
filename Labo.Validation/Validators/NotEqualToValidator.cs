namespace Labo.Validation.Validators
{
    using System;
    using System.Collections;

    using Labo.Validation.Message;

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
        /// The validator properties
        /// </summary>
        private readonly ValidatorProperties m_ValidatorProperties;

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
        {
            if (valueToCompare == null)
            {
                throw new ArgumentNullException("valueToCompare");
            }

            m_EqualToValidator = new EqualToValidator(valueToCompare, comparer);
            m_ValidatorProperties = new ValidatorProperties { { Constants.ValidationMessageParameterNames.VALUE_TO_COMPARE, ValueToCompare } };
        }

        /// <summary>
        /// Gets the type of the validator.
        /// </summary>
        /// <value>
        /// The type of the validator.
        /// </value>
        public override ValidatorType ValidatorType
        {
            get
            {
                return ValidatorType.NotEqualToValidator;
            }
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
        /// Gets the validation message.
        /// </summary>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// The validation message
        /// </returns>
        public override string GetValidationMessage(string valueName, params string[] arguments)
        {
            IValidationMessageBuilder messageBuilder = GetValidationMessageBuilder();
            string validationMessage = messageBuilder.SetMessageResourceName(Constants.ValidationMessageResourceNames.NOT_EQUAL_TO_VALIDATION_MESSAGE)
                                                     .SetValidatorProperties(m_ValidatorProperties)
                                                     .Build(valueName, arguments);

            return validationMessage;
        }

        /// <summary>
        /// Gets the validator properties.
        /// </summary>
        /// <returns>
        /// The validator properties.
        /// </returns>
        public override ValidatorProperties GetValidatorProperties()
        {
            return m_ValidatorProperties;
        }
    }
}
