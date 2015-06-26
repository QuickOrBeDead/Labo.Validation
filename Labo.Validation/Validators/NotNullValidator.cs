namespace Labo.Validation.Validators
{
    /// <summary>
    /// The not null validator class.
    /// </summary>
    public sealed class NotNullValidator : IValidator
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        public bool IsValid(object value)
        {
            return value != null;
        }
    }
}
