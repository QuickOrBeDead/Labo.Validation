namespace Labo.Validation
{
    using System;
    using System.Globalization;

    using Labo.Validation.Exceptions;

    /// <summary>
    /// The entity validator factory base class.
    /// </summary>
    public abstract class EntityValidatorFactoryBase : IValidatorFactory
    {
        /// <summary>
        /// Gets the validator for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The entity validator.</returns>
        public IEntityValidator<TEntity> GetValidatorFor<TEntity>()
        {
            return (IEntityValidator<TEntity>)GetValidatorFor(typeof(TEntity));
        }

        /// <summary>
        /// Gets the validator for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The entity validator.</returns>
        public IEntityValidator GetValidatorFor(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            IEntityValidator entityValidator = GetValidatorForOptional(type);

            if (entityValidator == null)
            {
                ValidatorFactoryException iocContainerValidatorFactoryException = new ValidatorFactoryException(string.Format(CultureInfo.CurrentCulture, "Entity validator for type: '{0}' could not be found.", type.FullName));
                throw iocContainerValidatorFactoryException;
            }

            return entityValidator;
        }

        /// <summary>
        /// Gets the validator for optional.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The entity validator.</returns>
        public abstract IEntityValidator GetValidatorForOptional(Type type);
    }
}