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
            : base(Constants.ValidationMessageResourceNames.LENGTH_VALIDATION_MESSAGE)
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
        /// Sets the validation message parameters.
        /// </summary>
        /// <param name="validationMessageBuilderParameterSetter">The validation message builder parameter setter.</param>
        protected override void SetValidationMessageParameters(IValidationMessageBuilderParameterSetter validationMessageBuilderParameterSetter)
        {
            if (validationMessageBuilderParameterSetter == null)
            {
                throw new ArgumentNullException("validationMessageBuilderParameterSetter");
            }

            validationMessageBuilderParameterSetter.SetParameter(Constants.ValidationMessageParameterNames.MIN, Min.ToStringInvariant())
                                                   .SetParameter(Constants.ValidationMessageParameterNames.MAX, Max.ToStringInvariant());
        }

        /// <summary>
        /// Gets the validation message builder parameter setter.
        /// </summary>
        /// <param name="validationMessageBuilder">The validation message builder.</param>
        /// <returns>The validation message</returns>
        protected override IValidationMessageBuilderParameterSetter GetValidationMessageBuilderParameterSetter(IValidationMessageBuilder validationMessageBuilder)
        {
            if (validationMessageBuilder == null)
            {
                throw new ArgumentNullException("validationMessageBuilder");
            }

            if (Max == -1)
            {
                return validationMessageBuilder.SetMessageFormat(GetValidationMessageFormat(Constants.ValidationMessageResourceNames.BIGGER_THAN_LENGTH_VALIDATION_MESSAGE));
            }

            if (Min == 0)
            {
                return validationMessageBuilder.SetMessageFormat(GetValidationMessageFormat(Constants.ValidationMessageResourceNames.SMALLER_THAN_LENGTH_VALIDATION_MESSAGE));
            }

            return base.GetValidationMessageBuilderParameterSetter(validationMessageBuilder);
        }
    }
}
