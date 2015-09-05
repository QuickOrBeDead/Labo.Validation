namespace Labo.Validation.Validators
{
    using Labo.Validation.Message;

    /// <summary>
    /// The not null validator class.
    /// </summary>
    public sealed class NotNullValidator : ValidatorBase
    {
        /// <summary>
        /// The static not null validator instance.
        /// </summary>
        private static readonly NotNullValidator s_Instance = new NotNullValidator();

        /// <summary>
        /// The validator properties
        /// </summary>
        private readonly ValidatorProperties m_ValidatorProperties;

        /// <summary>
        /// Gets the static not null validator instance.
        /// </summary>
        /// <value>
        /// The static not null validator instance.
        /// </value>
        public static NotNullValidator Instance
        {
            get
            {
                return s_Instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullValidator"/> class.
        /// </summary>
        public NotNullValidator()
        {
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
                return ValidatorType.NotNullValidator;
            }
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public override bool IsValid(object value)
        {
            return value != null;
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
            string validationMessage = messageBuilder.SetMessageResourceName(Constants.ValidationMessageResourceNames.NOT_NULL_VALIDATION_MESSAGE)
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
