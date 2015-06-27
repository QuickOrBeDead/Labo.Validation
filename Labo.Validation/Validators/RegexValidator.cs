namespace Labo.Validation.Validators
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The regex validator class.
    /// </summary>
    public class RegexValidator : IValidator
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
        public bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return m_Regex.IsMatch(value.ToString());
        }
    }
}
