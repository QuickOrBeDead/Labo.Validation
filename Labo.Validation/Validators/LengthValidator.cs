namespace Labo.Validation.Validators
{
    using System;

    using Labo.Validation.Message;

    /// <summary>
    /// The length validator class.
    /// </summary>
    public sealed class LengthValidator : ValidatorBase
    {
        /// <summary>
        /// The minimum length.
        /// </summary>
        private readonly int m_Min;

        /// <summary>
        /// The maximum length.
        /// </summary>
        private readonly int m_Max;

        /// <summary>
        /// The validator properties
        /// </summary>
        private readonly ValidatorProperties m_ValidatorProperties;

        /// <summary>
        /// Gets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public int Min
        {
            get
            {
                return m_Min;
            }
        }

        /// <summary>
        /// Gets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public int Max
        {
            get
            {
                return m_Max;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LengthValidator"/> class.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// min;Min should be larger than -1.
        /// or
        /// max;Max should be larger than min.
        /// </exception>
        public LengthValidator(int min, int max = -1)
        {
            if (min < 0)
            {
                throw new ArgumentOutOfRangeException("min", "Min should be larger than -1.");
            }

            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            m_Min = min;
            m_Max = max;

            m_ValidatorProperties = new ValidatorProperties
                                        {
                                            { Constants.ValidationMessageParameterNames.MIN, Min },
                                            { Constants.ValidationMessageParameterNames.MAX, Max }
                                        };
        }

        /// <summary>
        /// Creates the maximum length validator.
        /// </summary>
        /// <param name="max">The maximum length.</param>
        /// <returns>A new maximum length validator.</returns>
        public static LengthValidator CreateMaxLengthValidator(int max)
        {
            return new LengthValidator(0, max);
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
                return ValidatorType.LengthValidator;
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

            int length = value.ToString().Length;

            return length >= m_Min && (length <= m_Max || m_Max == -1);
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
            string validationMessageResourceName;
            if (Max == -1)
            {
                validationMessageResourceName = Constants.ValidationMessageResourceNames.BIGGER_THAN_LENGTH_VALIDATION_MESSAGE;
            }
            else if (Min == 0)
            {
                validationMessageResourceName = Constants.ValidationMessageResourceNames.SMALLER_THAN_LENGTH_VALIDATION_MESSAGE;
            }
            else
            {
                validationMessageResourceName = Constants.ValidationMessageResourceNames.LENGTH_VALIDATION_MESSAGE;
            }

            IValidationMessageBuilder messageBuilder = GetValidationMessageBuilder();
            string validationMessage = messageBuilder.SetMessageResourceName(validationMessageResourceName)
                                                     .SetValidatorProperties(m_ValidatorProperties)
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
