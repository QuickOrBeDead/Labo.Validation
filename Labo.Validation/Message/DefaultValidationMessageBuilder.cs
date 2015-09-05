namespace Labo.Validation.Message
{
    using System;
    using System.Collections.Generic;

    using Labo.Validation.Validators;

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
        /// The validation message resource manager
        /// </summary>
        private readonly IValidationMessageResourceManager m_ValidationMessageResourceManager;

        /// <summary>
        /// The parameters
        /// </summary>
        private readonly IDictionary<string, string> m_Parameters;

        /// <summary>
        /// The validation message format
        /// </summary>
        private string m_ValidationMessageFormat;

        /// <summary>
        /// Gets the validation message format.
        /// </summary>
        /// <value>
        /// The validation message format.
        /// </value>
        internal string ValidationMessageFormat
        {
            get { return m_ValidationMessageFormat; }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        internal IDictionary<string, string> Parameters
        {
            get { return m_Parameters; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValidationMessageBuilder"/> class.
        /// </summary>
        /// <param name="validationMessageFormatter">The validation message formatter.</param>
        /// <param name="validationMessageResourceManager">The validation message resource manager.</param>
        /// <exception cref="System.ArgumentNullException">validationMessageFormatter</exception>
        public DefaultValidationMessageBuilder(IValidationMessageFormatter validationMessageFormatter, IValidationMessageResourceManager validationMessageResourceManager)
        {
            if (validationMessageFormatter == null)
            {
                throw new ArgumentNullException("validationMessageFormatter");
            }

            if (validationMessageResourceManager == null)
            {
                throw new ArgumentNullException("validationMessageResourceManager");
            }

            m_ValidationMessageFormatter = validationMessageFormatter;
            m_ValidationMessageResourceManager = validationMessageResourceManager;
            m_Parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Sets the name of the message resource.
        /// </summary>
        /// <param name="validationMessageResourceName">Name of the validation message resource.</param>
        /// <returns>The validation message builder parameter setter.</returns>
        /// <exception cref="System.ArgumentNullException">validationMessageResourceName</exception>
        public IValidationMessageBuilderParameterSetter SetMessageResourceName(string validationMessageResourceName)
        {
            if (validationMessageResourceName == null)
            {
                throw new ArgumentNullException("validationMessageResourceName");
            }

            m_ValidationMessageFormat = m_ValidationMessageResourceManager.GetValidationMessageFormat(validationMessageResourceName);

            return this;
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
        /// <param name="valueName">
        /// The value Name.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The validation message.
        /// </returns>
        public string Build(string valueName, params string[] arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            if (string.IsNullOrWhiteSpace(m_ValidationMessageFormat))
            {
                throw new InvalidOperationException("Validation Message format cannot be null or empty.");
            }

            SetParameter(Constants.ValidationMessageParameterNames.VALUE_NAME, valueName);

            for (int i = 0; i < arguments.Length; i++)
            {
                string argument = arguments[i];

                SetParameter(i.ToStringInvariant(), argument);
            }

            return m_ValidationMessageFormatter.FormatMessage(m_ValidationMessageFormat, m_Parameters);
        }

        /// <summary>
        /// Sets the validator properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns>The validator properties.</returns>
        public IValidationMessageBuilderParameterSetter SetValidatorProperties(ValidatorProperties properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }

            IEnumerator<KeyValuePair<string, object>> enumerator = properties.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, object> validatorProperty = enumerator.Current;
                SetParameter(validatorProperty.Key, validatorProperty.Value.ToString());
            }

            return this;
        }
    }
}
