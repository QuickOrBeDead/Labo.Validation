namespace Labo.Validation.Validators
{
    using System;

    using Labo.Validation.Message;

    /// <summary>
    /// The predicate validator class.
    /// </summary>
    public sealed class PredicateValidator : ValidatorBase
    {
        /// <summary>
        /// The predicate
        /// </summary>
        private readonly Predicate<object> m_Predicate;

        /// <summary>
        /// The validator properties
        /// </summary>
        private readonly ValidatorProperties m_ValidatorProperties;

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>
        /// The predicate.
        /// </value>
        public Predicate<object> Predicate
        {
            get
            {
                return m_Predicate;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateValidator"/> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <exception cref="System.ArgumentNullException">predicate</exception>
        public PredicateValidator(Predicate<object> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            m_Predicate = predicate;
            m_ValidatorProperties = new ValidatorProperties();
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
                return ValidatorType.PredicateValidator;
            }
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public override bool IsValid(object value)
        {
            return m_Predicate(value);
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
            string validationMessage = messageBuilder.SetMessageResourceName(Constants.ValidationMessageResourceNames.PREDICATE_VALIDATION_MESSAGE)
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
