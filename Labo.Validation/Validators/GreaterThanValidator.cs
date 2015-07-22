namespace Labo.Validation.Validators
{
    using System;

    using Labo.Validation.Message;
    using Labo.Validation.Utils;

    /// <summary>
    /// The greater than validator class.
    /// </summary>
    public sealed class GreaterThanValidator : ValidatorBase
    {
        /// <summary>
        /// The value to compare
        /// </summary>
        private readonly IComparable m_ValueToCompare;

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
        /// Initializes a new instance of the <see cref="GreaterThanValidator"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <exception cref="System.ArgumentNullException">valueToCompare</exception>
        public GreaterThanValidator(IComparable valueToCompare)
            : base(Constants.ValidationMessageResourceNames.GREATER_THAN_VALIDATION_MESSAGE)
        {
            if (valueToCompare == null)
            {
                throw new ArgumentNullException("valueToCompare");
            }

            m_ValueToCompare = valueToCompare;
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
                return compareResult < 0;
            }

            return false;
        }

        /// <summary>
        /// Sets the validation message parameters.
        /// </summary>
        /// <param name="validationMessageBuilderParameterSetter">The validation message builder parameter setter.</param>
        protected override void SetValidationMessageParameters(IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter)
        {
            if (validationMessageBuilderParameterSetter == null)
            {
                throw new ArgumentNullException("validationMessageBuilderParameterSetter");
            }

            validationMessageBuilderParameterSetter.SetParameter(Constants.ValidationMessageParameterNames.VALUE_TO_COMPARE, ValueToCompare.ToString());
        }
    }
}