namespace Labo.Validation.Validators
{
    using Labo.Validation.Message;

    /// <summary>
    /// The credit card validator class.
    /// </summary>
    public sealed class CreditCardValidator : ValidatorBase
    {
        /// <summary>
        /// The static credit card validator instance.
        /// </summary>
        private static readonly CreditCardValidator s_Instance = new CreditCardValidator();

        /// <summary>
        /// The validator properties
        /// </summary>
        private readonly ValidatorProperties m_ValidatorProperties;

        /// <summary>
        /// Gets the static credit card validator instance.
        /// </summary>
        /// <value>
        /// The static credit card validator instance.
        /// </value>
        public static CreditCardValidator Instance
        {
            get
            {
                return s_Instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardValidator"/> class.
        /// </summary>
        public CreditCardValidator()
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
                return ValidatorType.CreditCardValidator;
            }
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

            // http://www.beachnet.com/~hstiles/cardtype.html
            string cardNumber = value.ToString()
                                     .Replace("-", string.Empty)
                                     .Replace(" ", string.Empty);

            int checksum = 0;
            bool evenDigit = false;
            
            char[] digits = cardNumber.ToCharArray();
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                char digit = digits[i];
                if (!char.IsDigit(digit))
                {
                    return false;
                }

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
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
            string validationMessage = messageBuilder.SetMessageResourceName(Constants.ValidationMessageResourceNames.CREDIT_CARD_VALIDATION_MESSAGE)
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
