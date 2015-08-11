namespace Labo.Validation
{
    using System;

    /// <summary>
    /// The default entity validator factory class.
    /// </summary>
    public sealed class DefaultEntityValidatorFactory : IValidatorFactory
    {
        /// <summary>
        /// Gets the validator for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The entity validator.</returns>
        public IEntityValidator<TEntity> GetValidatorFor<TEntity>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the validator for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The entity validator.</returns>
        public IEntityValidator GetValidatorFor(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
