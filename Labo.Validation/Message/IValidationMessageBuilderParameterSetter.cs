namespace Labo.Validation.Message
{
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
        /// Builds the validation message.
        /// </summary>
        /// <returns>The validation message.</returns>
        string Build();
    }
}