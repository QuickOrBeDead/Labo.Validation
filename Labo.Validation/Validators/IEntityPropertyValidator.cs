namespace Labo.Validation.Validators
{
    /// <summary>
    /// The entity property validator interface.
    /// </summary>
    public interface IEntityPropertyValidator
    {
        /// <summary>
        /// Determines whether the specified property value for the entity is valid.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <returns><c>true</c> if the specified property value for the entity is valid, otherwise <c>false</c></returns>
        bool IsValid(object entity, object propertyValue);

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The validation message</returns>
        string GetValidationMessage(object entity, string valueName, params string[] arguments);

        /// <summary>
        /// Gets the validator type name.
        /// </summary>
        /// <returns>The type name.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        string GetValidatorTypeName();

        /// <summary>
        /// Gets the validator properties.
        /// </summary>
        /// <returns>The validator properties.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        ValidatorProperties GetValidatorProperties();
    }
}