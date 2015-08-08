namespace Labo.Validation
{
    using System;

    /// <summary>
    /// The validator factory interface.
    /// </summary>
    public interface IValidatorFactory
    {
        /// <summary>
        /// Gets the validator for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The entity validator.</returns>
        IEntityValidator<TEntity> GetValidatorFor<TEntity>();

        /// <summary>
        /// Gets the validator for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The entity validator.</returns>
        IEntityValidator GetValidatorFor(Type type);
    }
}