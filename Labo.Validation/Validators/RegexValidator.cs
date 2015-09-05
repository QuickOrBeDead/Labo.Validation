namespace Labo.Validation.Validators
{
    using System;
    using System.Text.RegularExpressions;

    using Labo.Validation.Message;

    /// <summary>
    /// The regex validator class.
    /// </summary>
    public class RegexValidator : ValidatorBase
    {
        /// <summary>
        /// The validation message resource name
        /// </summary>
        private readonly string m_ValidationMessageResourceName;

        /// <summary>
        /// The regex
        /// </summary>
        private readonly Regex m_Regex;

        /// <summary>
        /// The validator properties
        /// </summary>
        private readonly ValidatorProperties m_ValidatorProperties;

        /// <summary>
        /// Gets the regex.
        /// </summary>
        /// <value>
        /// The regex.
        /// </value>
        public Regex Regex
        {
            get
            {
                return m_Regex;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexValidator"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="regexOptions">The regex options.</param>
        /// <exception cref="System.ArgumentNullException">expression</exception>
        public RegexValidator(string expression, RegexOptions regexOptions = RegexOptions.None)
            : this(Constants.ValidationMessageResourceNames.REGEX_VALIDATION_MESSAGE, expression, regexOptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexValidator"/> class.
        /// </summary>
        /// <param name="validationMessageResourceName">Name of the validation message resource.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="regexOptions">The regex options.</param>
        /// <exception cref="System.ArgumentNullException">expression</exception>
        public RegexValidator(string validationMessageResourceName, string expression, RegexOptions regexOptions = RegexOptions.None)
        {
            if (validationMessageResourceName == null)
            {
                throw new ArgumentNullException("validationMessageResourceName");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            m_Regex = new Regex(expression, RegexOptions.Compiled | regexOptions);
            m_ValidationMessageResourceName = validationMessageResourceName;
            m_ValidatorProperties = new ValidatorProperties { { Constants.ValidationMessageParameterNames.REGEX, expression } };
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
                return ValidatorType.RegexValidator;
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

            return m_Regex.IsMatch(value.ToString());
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
            string validationMessage = messageBuilder.SetMessageResourceName(m_ValidationMessageResourceName)
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
