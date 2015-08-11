﻿namespace Labo.Validation.Validators
{
    using System;
    using System.Globalization;

    using Labo.Validation.Message;

    /// <summary>
    /// The validator base class.
    /// </summary>
    public abstract class ValidatorBase : IValidator
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
        /// The validation message resource name
        /// </summary>
        private readonly string m_ValidationMessageResourceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorBase"/> class.
        /// </summary>
        /// <param name="validationMessageResourceName">Name of the validation message resource.</param>
        protected ValidatorBase(string validationMessageResourceName)
            : this(ValidatorSettings.ValidationMessageFormatter, ValidatorSettings.ValidationMessageResourceManager, validationMessageResourceName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorBase"/> class.
        /// </summary>
        /// <param name="validationMessageFormatter">The validation message formatter.</param>
        /// <param name="validationMessageResourceManager">The validation message resource manager.</param>
        /// <param name="validationMessageResourceName">Name of the validation message resource.</param>
        /// <exception cref="System.ArgumentNullException">
        /// validationMessageFormatter
        /// or
        /// validationMessageResourceManager
        /// or
        /// validationMessageResourceName
        /// </exception>
        protected ValidatorBase(IValidationMessageFormatter validationMessageFormatter, IValidationMessageResourceManager validationMessageResourceManager, string validationMessageResourceName)
        {
            if (validationMessageFormatter == null)
            {
                throw new ArgumentNullException("validationMessageFormatter");
            }

            if (validationMessageResourceManager == null)
            {
                throw new ArgumentNullException("validationMessageResourceManager");
            }

            if (validationMessageResourceName == null)
            {
                throw new ArgumentNullException("validationMessageResourceName");
            }

            m_ValidationMessageFormatter = validationMessageFormatter;
            m_ValidationMessageResourceManager = validationMessageResourceManager;
            m_ValidationMessageResourceName = validationMessageResourceName;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid, otherwise <c>false</c></returns>
        public abstract bool IsValid(object value);

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The validation message</returns>
        public string GetValidationMessage(string valueName, params string[] arguments)
        {
            if (valueName == null)
            {
                throw new ArgumentNullException("valueName");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            DefaultValidationMessageBuilder validationMessageBuilder = new DefaultValidationMessageBuilder(m_ValidationMessageFormatter);

            IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter = GetValidationMessageBuilderParameterSetter(validationMessageBuilder).SetParameter(Constants.ValidationMessageParameterNames.VALUE_NAME, valueName);

            for (int i = 0; i < arguments.Length; i++)
            {
                string argument = arguments[i];

                validationMessageBuilderParameterSetter.SetParameter(i.ToStringInvariant(), argument);
            }

            SetValidationMessageParameters(validationMessageBuilderParameterSetter);

            return validationMessageBuilderParameterSetter.Build();
        }

        /// <summary>
        /// Gets the validation message builder parameter setter.
        /// </summary>
        /// <param name="validationMessageBuilder">The validation message builder.</param>
        /// <returns>The validation message builder parameter setter.</returns>
        protected virtual IValidationMessageBuilderParameterSetter GetValidationMessageBuilderParameterSetter(IValidationMessageBuilder validationMessageBuilder)
        {
            if (validationMessageBuilder == null)
            {
                throw new ArgumentNullException("validationMessageBuilder");
            }

            string validationMessageFormat = GetValidationMessageFormat(m_ValidationMessageResourceName);

            return validationMessageBuilder.SetMessageFormat(validationMessageFormat);
        }

        /// <summary>
        /// Gets the validation message format.
        /// </summary>
        /// <param name="validationMessageResourceName">Name of the validation message resource.</param>
        /// <returns>The validation message format.</returns>
        protected string GetValidationMessageFormat(string validationMessageResourceName)
        {
            return m_ValidationMessageResourceManager.GetValidationMessageFormat(validationMessageResourceName) ?? string.Format(CultureInfo.CurrentCulture, "[NotFound:{0}]", validationMessageResourceName);
        }

        /// <summary>
        /// Sets the validation message parameters.
        /// </summary>
        /// <param name="validationMessageBuilderParameterSetter">The validation message builder parameter setter.</param>
        protected abstract void SetValidationMessageParameters(IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter);
    }
}
