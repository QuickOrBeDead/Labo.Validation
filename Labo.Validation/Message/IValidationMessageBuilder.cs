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
    }
}
