namespace Labo.Validation.Ioc
{
    using System;
    using System.Globalization;

    ///// <summary>
    ///// The ioc container valitation factory class.
    ///// </summary>
    //public sealed class IocContainerValidatorFactory : IValidatorFactory
    //{
    //    /// <summary>
    //    /// The ioc container resolver
    //    /// </summary>
    //    private readonly IIocContainerResolver m_IocContainerResolver;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="IocContainerValidatorFactory"/> class.
    //    /// </summary>
    //    /// <param name="iocContainerResolver">The ioc container resolver.</param>
    //    public IocContainerValidatorFactory(IIocContainerResolver iocContainerResolver) 
    //    {
    //        if (iocContainerResolver == null)
    //        {
    //            throw new ArgumentNullException("iocContainerResolver");
    //        }

    //        m_IocContainerResolver = iocContainerResolver;
    //    }

    //    /// <summary>
    //    /// Gets the validator for the specified entity type.
    //    /// </summary>
    //    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    //    /// <returns>The entity validator.</returns>
    //    public IEntityValidator<TEntity> GetValidatorFor<TEntity>()
    //    {
    //        return (IEntityValidator<TEntity>)GetValidatorFor(typeof(TEntity));
    //    }

    //    /// <summary>
    //    /// Gets the validator for the specified type.
    //    /// </summary>
    //    /// <param name="type">The type.</param>
    //    /// <returns>The entity validator.</returns>
    //    public IEntityValidator GetValidatorFor(Type type)
    //    {
    //        if (type == null)
    //        {
    //            throw new ArgumentNullException("type");
    //        }

    //        Type genericType = typeof(IEntityValidator<>).MakeGenericType(type);
    //        IEntityValidator entityValidator = m_IocContainerResolver.GetInstanceOptionalByName(genericType, type.FullName) as IEntityValidator;

    //        if (entityValidator == null)
    //        {
    //            IocContainerValidatorFactoryException iocContainerValidatorFactoryException = new IocContainerValidatorFactoryException(string.Format(CultureInfo.CurrentCulture, "Entity validator for type: '{0}'.", type.FullName));
    //            throw iocContainerValidatorFactoryException;
    //        }

    //        return entityValidator;
    //    }
    //}
}