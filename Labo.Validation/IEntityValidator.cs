namespace Labo.Validation
{
    /// <summary>
    /// The entity validator interface.
    /// </summary>
    public interface IEntityValidator
    {
        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        ValidationResult Validate(object entity);
    }

    /// <summary>
    /// The entity validator interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IEntityValidator<in TEntity> : IEntityValidator
    {
        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The validation result.</returns>
        ValidationResult Validate(TEntity entity);
    }
}
