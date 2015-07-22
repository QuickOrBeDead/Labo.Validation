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
            : base(Constants.ValidationMessageResourceNames.CREDIT_CARD_VALIDATION_MESSAGE)
        {
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
        /// Sets the validation message parameters.
        /// </summary>
        /// <param name="validationMessageBuilderParameterSetter">The validation message builder parameter setter.</param>
        protected override void SetValidationMessageParameters(IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter)
        {
        }
    }
}
