namespace Labo.Validation.Validators
{
    using Labo.Validation.Message;

    /// <summary>
    /// The validator base class.
    /// </summary>
    public abstract class ValidatorBase : IValidator
    {
        /// <summary>
        /// Gets the validation message builder.
        /// </summary>
        /// <returns>The validation message builder.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected internal static IValidationMessageBuilder GetValidationMessageBuilder()
        {
            return new DefaultValidationMessageBuilder(ValidatorSettings.ValidationMessageFormatter, ValidatorSettings.ValidationMessageResourceManager);
        }

        /// <summary>
        /// Gets the type of the validator.
        /// </summary>
        /// <value>
        /// The type of the validator.
        /// </value>
        public abstract ValidatorType ValidatorType { get; }

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
        public abstract string GetValidationMessage(string valueName, params string[] arguments);

        /// <summary>
        /// Gets the validator properties.
        /// </summary>
        /// <returns>The validator properties.</returns>
        public abstract ValidatorProperties GetValidatorProperties();
    }
}