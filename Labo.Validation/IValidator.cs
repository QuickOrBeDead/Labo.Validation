namespace Labo.Validation
{
    using Labo.Validation.Validators;

    /// <summary>
    /// The validator interface.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid, otherwise <c>false</c></returns>
        bool IsValid(object value);

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The validation message</returns>
        string GetValidationMessage(string valueName, params string[] arguments);

        /// <summary>
        /// Gets the validator properties.
        /// </summary>
        /// <returns>The validator properties.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        ValidatorProperties GetValidatorProperties();
    }
}