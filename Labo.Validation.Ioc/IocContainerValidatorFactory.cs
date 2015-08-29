namespace Labo.Validation.Ioc
{
    using System;

    using Labo.Common.Ioc;

    /// <summary>
    /// The ioc container valitation factory class.
    /// </summary>
    public sealed class IocContainerValidatorFactory : EntityValidatorFactoryBase
    {
        /// <summary>
        /// The ioc container resolver
        /// </summary>
        private readonly IIocContainer m_IocContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="IocContainerValidatorFactory"/> class.
        /// </summary>
        /// <param name="iocContainer">The ioc container.</param>
        public IocContainerValidatorFactory(IIocContainer iocContainer) 
        {
            if (iocContainer == null)
            {
                throw new ArgumentNullException("iocContainer");
            }

            m_IocContainer = iocContainer;
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

            Type genericType = typeof(IEntityValidator<>).MakeGenericType(type);
            IEntityValidator entityValidator = m_IocContainer.GetInstanceOptionalByName(genericType, type.FullName) as IEntityValidator;
            
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

            m_IocContainer.RegisterSingleInstanceNamed(x => validator, typeof(TEntity).FullName);
        }

        /// <summary>
        /// Registers the validator.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="validatorFunc">The validator function.</param>
        public void RegisterValidator<TEntity>(Func<IIocContainerResolver, IEntityValidator<TEntity>> validatorFunc)
        {
            if (validatorFunc == null)
            {
                throw new ArgumentNullException("validatorFunc");
            }

            m_IocContainer.RegisterSingleInstanceNamed(validatorFunc, typeof(TEntity).FullName);
        }
    }
}