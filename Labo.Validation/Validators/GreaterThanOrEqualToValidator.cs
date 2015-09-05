namespace Labo.Validation.Validators
{
    using System;

    using Labo.Validation.Message;
    using Labo.Validation.Utils;

    /// <summary>
    /// The greater than or equal to validator class.
    /// </summary>
    public sealed class GreaterThanOrEqualToValidator : ValidatorBase
    {
        /// <summary>
        /// The value to compare
        /// </summary>
        private readonly IComparable m_ValueToCompare;

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
        public IComparable ValueToCompare
        {
            get
            {
                return m_ValueToCompare;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanOrEqualToValidator"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <exception cref="System.ArgumentNullException">valueToCompare</exception>
        public GreaterThanOrEqualToValidator(IComparable valueToCompare)
        {
            if (valueToCompare == null)
            {
                throw new ArgumentNullException("valueToCompare");
            }

            m_ValueToCompare = valueToCompare;
            m_ValidatorProperties = new ValidatorProperties { { Constants.ValidationMessageParameterNames.VALUE_TO_COMPARE, ValueToCompare } };
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            IComparable comparableValue = value as IComparable;
            if (comparableValue == null)
            {
                return false;
            }

            int compareResult;
            if (ComparableUtils.TryCompareTo(m_ValueToCompare, comparableValue, out compareResult))
            {
                return compareResult <= 0;                
            }
            
            return false;
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
            string validationMessage = messageBuilder.SetMessageResourceName(Constants.ValidationMessageResourceNames.GREATER_THAN_OR_EQUAL_TO_VALIDATION_MESSAGE)
                                                     .SetValidatorProperties(m_ValidatorProperties)
                                                     .Build(valueName, arguments);

            return validationMessage;
        }

        /// <summary>
        /// Gets the validator properties.
        /// </summary>
        /// <returns>The validator properties.</returns>
        public override ValidatorProperties GetValidatorProperties()
        {
            return m_ValidatorProperties;
        }
    }
}
