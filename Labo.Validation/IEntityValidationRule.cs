namespace Labo.Validation
{
    /// <summary>
    /// The entity validation rule class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IEntityValidationRule<in TEntity>
    {
        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        ValidationResult Validate(TEntity entity);
    }
}
