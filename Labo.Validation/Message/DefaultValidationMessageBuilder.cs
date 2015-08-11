namespace Labo.Validation.Message
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The default validation message builder class.
    /// </summary>
    public sealed class DefaultValidationMessageBuilder : IValidationMessageBuilder, IValidationMessageBuilderParameterSetter
    {
        /// <summary>
        /// The validation message formatter
        /// </summary>
        private readonly IValidationMessageFormatter m_ValidationMessageFormatter;

        /// <summary>
        /// The parameters
        /// </summary>
        private readonly IDictionary<string, string> m_Parameters;

        /// <summary>
        /// The validation message format
        /// </summary>
        private string m_ValidationMessageFormat;

        /// <summary>
        /// The validation message format
        /// </summary>
        internal string ValidationMessageFormat
        {
            get { return m_ValidationMessageFormat; }
        }

        /// <summary>
        /// The parameters
        /// </summary>
        internal IDictionary<string, string> Parameters
        {
            get { return m_Parameters; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValidationMessageBuilder"/> class.
        /// </summary>
        /// <param name="validationMessageFormatter">The validation message formatter.</param>
        /// <exception cref="System.ArgumentNullException">validationMessageFormatter</exception>
        public DefaultValidationMessageBuilder(IValidationMessageFormatter validationMessageFormatter)
        {
            if (validationMessageFormatter == null)
            {
                throw new ArgumentNullException("validationMessageFormatter");
            }

            m_ValidationMessageFormatter = validationMessageFormatter;
            m_Parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Sets the message format.
        /// </summary>
        /// <param name="validationMessageFormat">The validation message format.</param>
        /// <returns>The validation message builder parameter setter.</returns>
        public IValidationMessageBuilderParameterSetter SetMessageFormat(string validationMessageFormat)
        {
            if (validationMessageFormat == null)
            {
                throw new ArgumentNullException("validationMessageFormat");
            }

            m_ValidationMessageFormat = validationMessageFormat;

            return this;
        }

        /// <summary>
        /// Sets the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns>The validation message builder parameter setter.</returns>
        public IValidationMessageBuilderParameterSetter SetParameter(string parameterName, string parameterValue)
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException("parameterName");
            }

            if (parameterValue == null)
            {
                throw new ArgumentNullException("parameterValue");
            }

            m_Parameters[parameterName] = parameterValue;

            return this;
        }

        /// <summary>
        /// Builds the validation message.
        /// </summary>
        /// <returns>The validation message.</returns>
        public string Build()
        {
            if (m_ValidationMessageFormat == null)
            {
                throw new InvalidOperationException("The validation message format cannot be null.");
            }

            return m_ValidationMessageFormatter.FormatMessage(m_ValidationMessageFormat, m_Parameters);
        }
    }
}
