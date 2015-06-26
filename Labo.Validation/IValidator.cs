namespace Labo.Validation
{
    /// <summary>
    /// The validator interface.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid otherwise <c>false</c></returns>
        bool IsValid(object value);
    }
}