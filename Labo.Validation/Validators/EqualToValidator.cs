namespace Labo.Validation.Validators
{
    using System;
    using System.Collections;

    using Labo.Validation.Message;
    using Labo.Validation.Utils;

    /// <summary>
    /// The equal to validator class.
    /// </summary>
    public sealed class EqualToValidator : ValidatorBase
    {
        /// <summary>
        /// The value to compare
        /// </summary>
        private readonly object m_ValueToCompare;

        /// <summary>
        /// The comparer
        /// </summary>
        private readonly IEqualityComparer m_Comparer;

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
                return m_ValueToCompare;
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
                return m_Comparer;
            }
        }

        /// <summary>
        /// Gets the type of the owner.
        /// </summary>
        /// <value>
        /// The type of the owner.
        /// </value>
        public Type OwnerType { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualToValidator"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="System.ArgumentNullException">valueToCompare</exception>
        public EqualToValidator(object valueToCompare, IEqualityComparer comparer = null)
        {
            if (valueToCompare == null)
            {
                throw new ArgumentNullException("valueToCompare");
            }

            m_ValueToCompare = valueToCompare;
            m_Comparer = comparer;
            m_ValidatorProperties = new ValidatorProperties { { Constants.ValidationMessageParameterNames.VALUE_TO_COMPARE, m_ValueToCompare } };
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
                return ValidatorType.EqualToValidator;
            }
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public override bool IsValid(object value)
        {
            object valueToCompare = m_ValueToCompare;
            IEqualityComparer equalityComparer = m_Comparer;
            return IsValid(value, valueToCompare, equalityComparer);
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="valueToCompare">The value automatic compare.</param>
        /// <param name="equalityComparer">The equality comparer.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        internal static bool IsValid(object value, object valueToCompare, IEqualityComparer equalityComparer)
        {
            return AreEqual(value, valueToCompare, equalityComparer);
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
            string validationMessage = messageBuilder.SetMessageResourceName(Constants.ValidationMessageResourceNames.EQUAL_TO_VALIDATION_MESSAGE)
                                                     .SetValidatorProperties(m_ValidatorProperties)
                                                     .Build(valueName, arguments);

            return validationMessage;
        }

        /// <summary>
        /// Determines whether [are equal] [the specified values].
        /// </summary>
        /// <param name="sourceValue">The source value.</param>
        /// <param name="destinationValue">The destination value.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns><c>true</c> if the specified values are equal otherwise <c>false</c></returns>
        private static bool AreEqual(object sourceValue, object destinationValue, IEqualityComparer comparer = null)
        {
            if (comparer != null)
            {
                return comparer.Equals(sourceValue, destinationValue);
            }

            IComparable sourceComparable = sourceValue as IComparable;
            if (sourceComparable != null)
            {
                IComparable destinationComparable = destinationValue as IComparable;
                if (destinationComparable != null)
                {
                    int compareResult;
                    if (ComparableUtils.TryCompareTo(sourceComparable, destinationComparable, out compareResult))
                    {
                        return compareResult == 0;
                    }
                }
            }

            return sourceValue == destinationValue;
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
