namespace Labo.Validation.Attributes
{
    /// <summary>
    /// The validation attribute interface.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public interface IValidationAttribute
    {
        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <returns>The validator.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IValidator GetValidator();
    }
}