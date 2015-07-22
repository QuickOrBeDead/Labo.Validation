namespace Labo.Validation.Validators
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// The phone number validator.
    /// </summary>
    public sealed class PhoneNumberValidator : RegexValidator
    {
        /// <summary>
        /// The phone number regex expression.
        /// </summary>
        private const string EXPRESSION = "^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$";

        /// <summary>
        /// The static phone number validator instance.
        /// </summary>
        private static readonly PhoneNumberValidator s_Instance = new PhoneNumberValidator();

        /// <summary>
        /// Gets the static phone number validator instance.
        /// </summary>
        /// <value>
        /// The static phone number validator instance.
        /// </value>
        public static PhoneNumberValidator Instance
        {
            get
            {
                return s_Instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberValidator"/> class.
        /// </summary>
        public PhoneNumberValidator()
            : base(Constants.ValidationMessageResourceNames.PHONE_NUMBER_VALIDATION_MESSAGE, EXPRESSION, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture)
        {
        }
    }
}
