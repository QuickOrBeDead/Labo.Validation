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
        /// The regex
        /// </summary>
        private readonly Regex m_Regex;

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
            : base(validationMessageResourceName)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            m_Regex = new Regex(expression, RegexOptions.Compiled | regexOptions);
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
        /// Sets the validation message parameters.
        /// </summary>
        /// <param name="validationMessageBuilderParameterSetter">The validation message builder parameter setter.</param>
        protected override void SetValidationMessageParameters(IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter)
        {
        }
    }
}
