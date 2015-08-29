namespace Labo.Validation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The default entity validator factory class.
    /// </summary>
    public sealed class DefaultEntityValidatorFactory : EntityValidatorFactoryBase
    {
        /// <summary>
        /// The m_ validators
        /// </summary>
        private readonly IDictionary<string, IEntityValidator> m_Validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultEntityValidatorFactory"/> class.
        /// </summary>
        public DefaultEntityValidatorFactory()
        {
            m_Validators = new SortedList<string, IEntityValidator>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the validator for optional.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The entity validator.</returns>
        public override IEntityValidator GetValidatorForOptional(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            IEntityValidator entityValidator;
            m_Validators.TryGetValue(type.FullName, out entityValidator);

            return entityValidator;
        }

        /// <summary>
        /// Registers the validator.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="validator">The validator.</param>
        public void RegisterValidator<TEntity>(IEntityValidator<TEntity> validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            m_Validators.Add(typeof(TEntity).FullName, validator);
        }
    }
}
