namespace Labo.Validation
{
    using System;

    /// <summary>
    /// The entity validator factory interface.
    /// </summary>
    public interface IEntityValidatorFactory
    {
        /// <summary>
        /// Gets the entity validator for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The entity validator.</returns>
        IEntityValidator<TEntity> GetValidator<TEntity>();

        /// <summary>
        /// Gets the validator for the specified entity type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The entity validator.</returns>
        IEntityValidator GetValidator(Type type);
    }
}
