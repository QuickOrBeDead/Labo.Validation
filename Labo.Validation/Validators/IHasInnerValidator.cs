namespace Labo.Validation.Validators
{
    /// <summary>
    /// The has inner validator interface.
    /// </summary>
    public interface IHasInnerValidator
    {
        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        IValidator InnerValidator { get; }
    }
}