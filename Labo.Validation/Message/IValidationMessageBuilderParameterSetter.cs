namespace Labo.Validation.Message
{
    using Labo.Validation.Validators;

    /// <summary>
    /// The validation message builder parameter setter interface.
    /// </summary>
    public interface IValidationMessageBuilderParameterSetter
    {
        /// <summary>
        /// Sets the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns>The validation message builder parameter setter.</returns>
        IValidationMessageBuilderParameterSetter SetParameter(string parameterName, string parameterValue);

        /// <summary>
        /// Sets the validator properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns>The validator properties.</returns>
        IValidationMessageBuilderParameterSetter SetValidatorProperties(ValidatorProperties properties);

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
        string Build(string valueName, params string[] arguments);
    }
}