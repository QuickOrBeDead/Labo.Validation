namespace Labo.Validation.Message
{
    /// <summary>
    /// The validation message builder interface.
    /// </summary>
    public interface IValidationMessageBuilder
    {
        /// <summary>
        /// Sets the message format.
        /// </summary>
        /// <param name="validationMessageFormat">The validation message format.</param>
        /// <returns>The validation message builder parameter setter.</returns>
        IValidationMessageBuilderParameterSetter SetMessageFormat(string validationMessageFormat);

        /// <summary>
        /// Sets the name of the message resource.
        /// </summary>
        /// <param name="validationMessageResourceName">Name of the validation message resource.</param>
        /// <returns>The validation message builder parameter setter.</returns>
        /// <exception cref="System.ArgumentNullException">validationMessageResourceName</exception>
        IValidationMessageBuilderParameterSetter SetMessageResourceName(string validationMessageResourceName);
    }
}
