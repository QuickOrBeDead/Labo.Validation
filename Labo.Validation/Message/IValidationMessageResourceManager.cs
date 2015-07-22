namespace Labo.Validation.Message
{
    /// <summary>
    /// The validation message resource manager interface.
    /// </summary>
    public interface IValidationMessageResourceManager
    {
        /// <summary>
        /// Gets the validation message format.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The validation message format.</returns>
        string GetValidationMessageFormat(string resourceName);
    }
}